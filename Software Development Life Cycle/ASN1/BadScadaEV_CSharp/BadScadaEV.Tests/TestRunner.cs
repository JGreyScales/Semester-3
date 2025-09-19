using System;
using System.Collections.Generic;

static class AssertX {
    public static void Equal<T>(T expected, T actual, string? msg=null) {
        if (!Equals(expected, actual)) {
            throw new Exception($"ASSERT Equal failed. Expected: {expected}, Actual: {actual}. {msg}");
        }
    }
    public static void True(bool cond, string? msg=null) {
        if (!cond) throw new Exception($"ASSERT True failed. {msg}");
    }
}

class TestRegistry {
    public record Test(string Name, Action Fn);
    private static readonly List<Test> _tests = new();
    public static void Add(string name, Action fn) => _tests.Add(new Test(name, fn));
    public static int RunAll() {
        int pass = 0;
        foreach (var t in _tests) {
            try { t.Fn(); Console.WriteLine($"[PASS] {t.Name}"); pass++; }
            catch (Exception ex) { Console.WriteLine($"[FAIL] {t.Name} :: {ex.Message}"); }
        }
        Console.WriteLine($"\n{pass} test(s) passed.");
        return pass;
    }
}

static class Test { public static void Define(string name, Action fn) => TestRegistry.Add(name, fn); }

class Program {
    static void Main(string[] args) {
        // Example AAA tests students can revise/expand

        Test.Define("Unit_LoadShedding_TriggersWhenExceedingSiteLimit", () => {
            // Arrange
            double siteLimit = 150.0;
            double currentSiteKw = 140.0;
            double incoming = 20.0;

            // Act
            bool shouldShed = currentSiteKw + incoming > siteLimit;
            double shedAmount = shouldShed ? (currentSiteKw + incoming - siteLimit) : 0.0;

            // Assert
            AssertX.True(shouldShed, "Should shed when over limit.");
            AssertX.Equal(10.0, Math.Round(shedAmount,1));
        });

        Test.Define("Integration_SessionStart_ProducesHmiAndLog", () => {
            // Arrange
            var log = new List<string>();
            void Hmi(string s) => log.Add("HMI:" + s);
            void Logger(string s) => log.Add("LOG:" + s);

            // Act
            Hmi("A1 OK: 45.0 kW at 40 c/kWh");
            Logger("PRICE:A1:40");

            // Assert
            AssertX.Equal(2, log.Count);
            AssertX.True(log[0].StartsWith("HMI"));
            AssertX.True(log[1].StartsWith("LOG"));
        });

        TestRegistry.RunAll();
    }
}
