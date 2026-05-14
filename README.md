# TraceFlow API 🚀
Backend API developed with **.NET 8** focused on **inventory and stock movement management**, implementing modern backend practices such as JWT Authentication, Docker, Observability, Distributed Tracing, Health Checks, and CI/CD.
---
## 📌 Features
- 🔐 JWT Authentication
- 📦 Product Management
- 🏬 Warehouse Management
- 📊 Stock Movement Tracking
- 🐳 Dockerized Environment
- 📜 Structured Logging with Serilog
- 📡 Distributed Tracing with OpenTelemetry + Jaeger
- 📈 Log Visualization with Seq
- 🩺 Health Checks
- ✅ Unit Tests with xUnit
- ⚙️ CI/CD with GitHub Actions
---
## 🛠️ Tech Stack
| Technology | Purpose |
|---|---|
| .NET 8 | Framework |
| ASP.NET Core Web API | API Layer |
| Entity Framework Core | ORM |
| PostgreSQL | Database |
| Docker & Docker Compose | Containerization |
| JWT Authentication | Security |
| Serilog | Structured Logging |
| Seq | Log Visualization |
| OpenTelemetry + Jaeger | Distributed Tracing |
| xUnit | Unit Testing |
| GitHub Actions | CI/CD |
---
## 🏗️ Architecture
The project follows a **layered architecture** pattern:
```
TraceFlow-Training
│
├── TraceFlowTraining.Domain          # Entities, Interfaces, Enums, Business Rules
├── TraceFlowTraining.Application     # DTOs, Application Services, Use Cases
├── TraceFlowTraining.Infrastructure  # Repositories, Security, Logging, Persistence
├── TraceFlowTraining.Tests           # Unit Tests
└── TraceFlow-Training                # API: Controllers, Endpoints, DI, Swagger
```
### Layers
**Domain**
- Entities
- Interfaces
- Enums
- Business rules
  **Application**
- DTOs
- Application services
- Use cases
  **Infrastructure**
- Database access
- Repositories
- Security
- Logging
- Persistence
  **API**
- Controllers
- Endpoints
- Authentication
- Swagger
- Dependency Injection
---
## 🔐 Authentication
The API uses **JWT ******
### Login Endpoint
```http
POST /api/Auth/login
```
**Request body:**
```json
{
  "username": "admin",
  "password": "123456"
}
```
---
## 📦 Endpoints
### Product
```http
POST /api/Product
```
```json
{
  "name": "Notebook Gamer",
  "description": "RTX 4070 - 32GB RAM",
  "sku": "NOTE-RTX-4070",
  "price": 8999.90,
  "quantity": 15
}
```
### Warehouse
```http
POST /api/Warehouse
```
```json
{
  "name": "Warehouse Orlando",
  "location": "Florida - USA",
  "capacity": 500
}
```
### Stock Movement
```http
POST /api/StockMovement
```
```json
{
  "productId": "e6903dd8-f5ea-401e-aa46-5304040c2b4c",
  "quantity": 5,
  "type": 0
}
```
**Movement Types:**
| Type | Description |
|---|---|
| `0` | Stock Entry |
| `1` | Stock Exit |
---
## 🐳 Running with Docker
Build and start all containers:
```bash
docker compose up -d --build
```
---
## 📡 Observability
| Service | URL |
|---|---|
| Seq (Logs) | http://localhost:5341 |
| Jaeger (Tracing) | http://localhost:16686 |
| Health Check | http://localhost:8080/health |
---
## 🧪 Running Tests
```bash
dotnet test
```
---
## ⚙️ CI/CD
GitHub Actions pipeline configured for:
- ✅ Build
- ✅ Restore
- ✅ Test execution
---
## 🚀 Future Improvements
- [ ] Redis Cache
- [ ] RabbitMQ Integration
- [ ] Kubernetes Deployment
- [ ] Datadog Integration
- [ ] Rate Limiting
- [ ] Role-based Authorization
---
## 👨‍💻 Author
Developed by **Gustavo Gomes**.