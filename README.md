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
- logowanie do serwisu z podziałem na role klient/pracownik/administrator (autoryzacja JWT)
- seed sal kinowych, seansów, filmów 
- rezerwacja biletu i miejsca na seans
- API do pobierania informacji o filmach z serwisu
- API do pobierania informacji o zarezerwowanych biletach 

## Dokumentacja API
Dostęp do endpointów po wcześniejszej autoryzacji poprzez JWT/Bearer Token
Endpointy zwracają dane w formacie JSON

Get list of:
-> Lista sal:
https://localhost:7001/api/halls

-> Lista filmów:
https://localhost:7001/api/movies

-> Lista seansów:
https://localhost:7001/api/screenings

-> Lista rezerwacji:
https://localhost:7001/api/bookings


## Harmonogram
