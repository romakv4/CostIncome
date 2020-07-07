[![Build Status](https://travis-ci.com/romakv4/CostIncome.svg?branch=develop)](https://travis-ci.com/romakv4/CostIncome)

# Server launch requirements
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

# Client launch instructions
1. Navigate to `client` folder
2. Execute `npm install` command
3. Execute `npm start` command

# Testing and reporting
Use `npm run e2e` for open and run cypress tests in manual mode.  
Use `npm test` for run cypress tests in electron browser and generate report about testing session. Generated report is available in `client/cypress/reports/mochareports/` folder.