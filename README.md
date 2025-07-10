# EventsWebApp API

A REST API for event management built with ASP.NET Core 8.0, featuring:
- Policy-based authorization with jwt access and refresh tokens
- FluentValidation
- Docker support

## 🚀 Quick Start

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

### 1. Create the container
Navigate to your solution root directory (where .sln file is located)
```bash
cd path/to/your/solution
```
Build with proper context
```bash
docker build -t events-api -f EventsWepApp.API/Dockerfile .
```
### 2. Container management
To run a new container use
```bash 
docker run -p 8080:8080 --name events-container events-api
```
To stop the container use
```bash
docker container stop events-container
```
To restart the container use
```bash
docker container restart events-container
```


### 3. Open web site
Visit this link
```bash
http://localhost:8080/swagger/
```
For testing purposes there're two user accounts:
1. Admin (test@gmail.com : 123456)
2. User (user@gmail.com : 123456)

Use the credentials above in POST auth/login endpoint to get the access and refresh tokens.<br>
Then pass the access token to "Authorize" menu to pass through the auth policy.<br>
