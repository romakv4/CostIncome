[![Build Status](https://travis-ci.com/romakv4/CostIncome.svg?branch=develop)](https://travis-ci.com/romakv4/CostIncome)

# Launch requirements
1. .Net core
2. appsettings.Development.json and appsettings.json files in CostIncomeCalculator folder  

### appsettings.json
```JSON
{ 
    "Logging": 
    {
        "LogLevel": 
        { 
            "Default": "Debug", 
            "System": "Information", 
            "Microsoft": "Information" 
        } 
    }
}
```

### appsettings.Development.json
```JSON
{ 
    "AppSettings": { 
        "Token": "Secret for JWT authentication" 
    }, 
    "ConnectionStrings": { 
        "DefaultConnection": "Database connection string" 
    }, 
    "Logging": { 
        "LogLevel": { 
            "Default": "Warning" 
        } 
    }, 
    "AllowedHosts": "*"
}
```
3. PostgreSQL server
4. Create SQL database and apply migrations