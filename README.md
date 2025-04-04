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

```bash
# Navigate to your solution root directory (where .sln file is located)
cd path/to/your/solution
```
```bash
# Build with proper context
docker build -t events-api -f EventsWepApp.API/Dockerfile .
```
### 2. Container management
```bash 
# To run a new container use
docker run -p 8080:8080 --name events-container events-api
```
```bash
# To stop the container use
docker container stop events-container
```
```bash
# To restart the container use
docker container restart events-container
```


### 3. Open web site
```bash
# Visit this link
http://localhost:8080/swagger/
```
For testing purposes there're two user accounts:
1. Admin (test@gmail.com : 123456)
2. User (user@gmail.com : 123456)

Use the credentials above in POST auth/login endpoint to get the access and refresh tokens.<br>
Then pass the access token to "Authorize" menu to pass through the auth policy.