[![Build Status](https://travis-ci.com/romakv4/CostIncome.svg?branch=develop)](https://travis-ci.com/romakv4/CostIncome)

# Launch requirements
1. .Net core
2. Mailgun account
3. appsettings.Development.json and appsettings.json files in CostIncomeCalculator folder  

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
    "Mailgun": {
        "Domain": "Your mailgun domain",
        "APIkey": "Your mailgun API key"
    }, 
    "Logging": { 
        "LogLevel": { 
            "Default": "Warning" 
        } 
    }, 
    "AllowedHosts": "*"
}
```
4. PostgreSQL server
5. Create SQL database and apply migrations