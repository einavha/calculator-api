
# 🧮 Calculator API

A simple REST API for performing arithmetic operations on two numbers, secured with JWT and documented using Swagger.

---

## 🔧 Technologies Used

- ASP.NET Core 8.0
- JWT Authentication
- Swagger / OpenAPI (YAML definition)
- xUnit for Unit Testing
- Docker + Docker Compose

---

## 🚀 Getting Started

### 1. Clone and Run with Docker:

```bash
git clone https://github.com/einavha/calculator-api.git
cd Calculator-api/src/SimpleCalculatorService
docker-compose up --build

### 2. Access the API:
- **Base URL**: `http://localhost:5000/`
	- **Authentication**: Use the `/api/v1/login` endpoint to obtain a JWT token. Include this token in the `Authorization` header for all subsequent requests.
	- **Use API**: use the `/api/v1/calculate` endpoint to POST number1, number2 and the xOperation Header [Add, Subtract, Multiply, Divide] to get the calculated result.
	- **Swagger UI**: `http://localhost:5000/swagger/index.html`
	- **Postman Collection**:  file: postman_collection.json

