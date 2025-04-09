# UrlShortener-APi-asp.net
Basic URL shortener API that supports random or custom URLs, generates QR codes for links, and provides (very basic for the moment) engagement data for the shortened URLs.


## GETTING STARTED 

### Set the connection string with secret manager

```powershell
    dotnet user-secrets set "ConnectionString:ExerciseDbConnectionString" "Server=[ServernName] or localhost;Database=[DatabaseName];User Id=user;Password=[PASSWORD GOES HERE];TrustServerCertificate=True;"
```

### Set up the database
```powershell
    dotnet ef database update
```

## AUTHOR
me: (https://github.com/CannaAz)

## LICENSE
[MIT License] (https://github.com/CannaAz/UrlShortener-APi-asp.net/blob/dev/LICENSE)

