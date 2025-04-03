# UrlShortener-APi-asp.net
Basic URL shortener API that supports random or custom URLs, generates QR codes for links, and provides (very basic for the moment) engagement data for the shortened URLs.


## GETTING STARTED 

### Set the Sqlite database 
```powershell
    dotnet ef database update
```

### Set the connection string with secret manager

```powershell
    dotnet user-secrets set "ConnectionString:DatabaseConnectionStr" "Data Source=DatabaseName.db"
```

## AUTHOR
me: (https://github.com/CannaAz)

## LICENSE
[MIT License] (https://github.com/CannaAz/UrlShortener-APi-asp.net/blob/dev/LICENSE)

