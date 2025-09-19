using System;
using System.Collections.Generic;
using System.Threading;
using static System.Console;


static class Globals {
    public static List<string> gLog = new();
    public static Dictionary<string, object> db = new(); 
    public static double siteLimitKw = 150; 
}

class ChargerPacket {
    public string ChargerId = "";
    public bool Occupied = false;
    public bool Fault = false;
    public double PowerKw = 0;
    public double PriceCentsPerKwh = 0;
    public DateTime Ts = DateTime.UtcNow;
}

class PlazaController {
    private Random _rand = new Random(); 
    private Dictionary<string, object> _chargers = new(); 

    public void Run() {
        Print("Starting EV Plaza Controller...");
        _chargers["A1"] = new { Id="A1", Proto="ocpp", MaxKw=75.0, Status="idle" };
        _chargers["A2"] = new { Id="A2", Proto="wifi-json", MaxKw=75.0, Status="idle" };
        _chargers["B1"] = new { Id="B1", Proto="ocpp", MaxKw=50.0, Status="idle" };

        for (int i=0;i<8;i++) {

            var p1 = Poll("A1");
            HandlePacket(p1);

            var p2 = Poll("A2");
            HandlePacket(p2);

            var p3 = Poll("B1");
            HandlePacket(p3);

            Thread.Sleep(200); 
        }

        DumpLog();
        Print("Controller stopped.");
    }

    private ChargerPacket Poll(string id) {

        bool occupied = _rand.Next(0,3) != 0; 
        bool fault = _rand.Next(0,20) == 0;   
        double draw = occupied && !fault ? Math.Round(10 + _rand.NextDouble()*60, 1) : 0.0;
        double price = 35 + _rand.Next(0, 16); 
        return new ChargerPacket { ChargerId=id, Occupied=occupied, Fault=fault, PowerKw=draw, PriceCentsPerKwh=price, Ts=DateTime.UtcNow };
    }

    private void HandlePacket(ChargerPacket p) {
    
        var cfg = _chargers[p.ChargerId];
        string proto = (string)cfg.GetType().GetProperty("Proto").GetValue(cfg);
        double maxKw = (double)cfg.GetType().GetProperty("MaxKw").GetValue(cfg);


        if (p.Fault) {
            Print($"[HMI] Fault at charger {p.ChargerId}");
            Globals.gLog.Add($"FAULT:{p.ChargerId}:{p.Ts:o}");
            SaveEvent($"db:fault:{p.ChargerId}:{p.Ts:o}");
            return;
        }

  
        double currentSiteKw = GetSitePower();
        if (p.Occupied && p.PowerKw > 0) {
            if (currentSiteKw + p.PowerKw > Globals.siteLimitKw) {
                double shed = Math.Max(0, currentSiteKw + p.PowerKw - Globals.siteLimitKw);
                Print($"[HMI] Shedding {shed:0.0} kW at {p.ChargerId} (site limit {Globals.siteLimitKw} kW)");
                Globals.gLog.Add($"SHED:{p.ChargerId}:{shed:0.0}");
     
                if (proto == "ocpp") {
                    Print($"Send OCPP SetChargingProfile {{ limitKw: {(maxKw - shed):0.0} }}");
                } else {
                    Print($"Send WiFi JSON: {{\"cmd\":\"throttle\",\"limit\":{(maxKw - shed):0.0}}}");
                }
                Globals.db[$"{p.ChargerId}:limit"] = maxKw - shed;
            } else {
                Print($"[HMI] {p.ChargerId} OK: {p.PowerKw:0.0} kW at {p.PriceCentsPerKwh} c/kWh");
            }
        } else if (!p.Occupied) {
            Print($"[HMI] {p.ChargerId} idle");
        }

  
        Globals.db[$"{p.ChargerId}:price"] = p.PriceCentsPerKwh;
        Globals.gLog.Add($"PRICE:{p.ChargerId}:{p.PriceCentsPerKwh}");
    }

    private double GetSitePower() {

        double a1 = Globals.db.ContainsKey("A1:kw") ? (double)Globals.db["A1:kw"] : 0.0;
        double a2 = Globals.db.ContainsKey("A2:kw") ? (double)Globals.db["A2:kw"] : 0.0;
        double b1 = Globals.db.ContainsKey("B1:kw") ? (double)Globals.db["B1:kw"] : 0.0;
        return a1 + a2 + b1;
    }

    private void SaveEvent(string row) {

        Globals.db["lastEvent"] = row;
    }

    private void Print(string msg) {
        WriteLine(msg);
    }

    private void DumpLog() {
        WriteLine("\n--- LOG ---");
        foreach (var s in Globals.gLog) WriteLine(s);
    }
}

class Program {
    static void Main(string[] args) {
        new PlazaController().Run();
    }
}
