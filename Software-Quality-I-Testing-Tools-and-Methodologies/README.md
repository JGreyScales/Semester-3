# Asn1
Coffee Shop Order Builder (Unit Tests + TDD)

# Asn2
## 2a 
CFG and Linearly Independant Pathing (Knowledge base)
## 2b
Selenium + C#


# SETUP STEPS
``DOTNET 9.0`` / ```DOTNET 8.0```

Nothing here is advanced enough to require a specific version

DOTNET folder on path & at the top of the list

``dotnet new console -n {name}``

``dotnet new mstest -n {test_name}``

``cd {test_name}``

``dotnet add reference ..\{name}\{name}.csproj``

``dotnet test --logger "trx;LogFileName=TestResults{num}.trx"  --results-directory {fullpath to result dir}``

``dotnet run --project {proj_dir}``

``dotnet clean``