Note: The server must be ran first.

To compile the TCP WIN server:
```g++ '.\TCP WIN server.cpp' -lws2_32 -o server.exe```  
```.\server.exe```


To compile the TCP WIN client:
```g++ '.\TCP WIN client.cpp' -lws2_32 -o client.exe```  
```.\client.exe```


The Ubuntu material expects only one of the services to run on the virtual machine, the other service is to be ran on the primary.  
This means a custom NAT card with port forwarding needs to be configured for it to execute as expected  

Protocol: TCP  
Host IP: 127.0.0.1  
Host Port:27500  
Guest IP: 10.5.5.20  
Guest Port:27000  


To compile the TCP UBUNTU client on virtual machine:  
```g++ TCP\ UBUNTU\ client.cpp```
```./a.out```

To compile the TCP UBUNTU server on virtual machine:  
```g++ TCP\ UBUNTU\ server.cpp```
```./a.out```
