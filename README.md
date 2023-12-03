# CinemaApp
Cinema app to book ticket for seans

## Technologie
### Frontend
- nodeJS, Angular

### BackEnd
- .net, C#

### Baza danych
- PostgreSQL

## Funkcjonalności
- logowanie do serwisu (autoryzacja JWT)
- seed sal kinowych, seansów, filmów 
- rezerwacja biletu i miejsca na seans
- API do pobierania informacji o filmach z serwisu
- API do pobierania informacji o zarezerwowanych biletach 

## Connection string 
"DbConnectionString": "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=postgres"

## Dane do autentykacji
email: user@gmail.com  
password: User321123  

`LUB`     
email: cinema@gmail.com  
password: Cinema321123

## Endpoint do uzyskania Bearer Tokenu
https://localhost:7001/api/account/login   

W body requesta POST należy zawrzeć email i password użytkownika w formacie JSON. W odpowiedzi zwrotnej otrzyma się między innymi token potrzebny do otrzymania informacji z innych endpointów.
 ```json
  {
    "email": "user@gmail.com",
    "password": "User321123"
}
  ```
## Dokumentacja API
Dostęp do endpointów po wcześniejszej autoryzacji poprzez JWT/Bearer Token. 
Endpointy zwracają dane w formacie JSON.

-> Lista sal:
https://localhost:7001/api/halls  
--> Pojedynczy rekord o numerze id (np.: 1):  
https://localhost:7001/api/halls/1  
---> Dodanie nowej sali (request POST):  
https://localhost:7001/api/halls  
 ```json
  {
        "Name": "New hall",
        "SeatsPerRow": 76,
        "Rows": 122
}
  ```
----> Update sali o numerze id (np.: 1) (request PUT):  
```json
  {
        "Id": "7",
        "Name": "Big hallings",
        "SeatsPerRow": 72,
        "Rows": 12
}
  ```

-> Lista filmów:
https://localhost:7001/api/movies

-> Lista seansów:
https://localhost:7001/api/screenings

-> Lista rezerwacji:
https://localhost:7001/api/bookings
