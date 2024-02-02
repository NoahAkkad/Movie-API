# Movie API Documentation

## Table of Contents

- [Introduction](#introduction)
- [Code Overview](#code-overview)
- [API Requests](#api-requests)
- [Analysis and Reflection](#analysis-and-reflection)

## Introduction

This document provides detailed documentation for the Movie API developed using ASP.NET Core, Entity Framework Core, and ASP.NET Core Identity. The API enables the storage and management of movies and associated reviews, with authentication and authorization using ASP.NET Core Identity.

## Code Overview

### Data Models

#### `Movie` Class (`MovieApi.Models.Movie`)

```csharp
public class Movie
{
    // Properties
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    [ValidateNever]
    public ICollection<Review> Reviews { get; set; }
}
```

#### `Review` Class (`MovieApi.Models.Review`)

```csharp
public class Review
{
    // Properties
    [Key]
    public int Id { get; set; }
    [Required]
    public string Rating { get; set; }
    public string Comment { get; set; }
    [ValidateNever]
    public string MovieName { get; set; }
    [ValidateNever]
    [ForeignKey("Movie")]
    public int MovieId { get; set; }
    [ValidateNever]
    [JsonIgnore]
    public Movie Movie { get; set; }
    // Additional property for user association
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    [ValidateNever]
    [JsonIgnore]
    public CustomUser User { get; set; }
}
```

#### `CustomUser` Class (`MovieApi.Models.CustomUser`)

```csharp
public class CustomUser : IdentityUser
{
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
```

### Database Context

#### `DatabaseContext` Class (`MovieApi.Data.DatabaseContext`)

```csharp
public class DatabaseContext : IdentityDbContext<CustomUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
}
```

### API Controllers

#### `MoviesController` Class (`MovieApi.Controllers.MoviesController`)

```csharp
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    // ... (CRUD operations for movies)
}
```

#### `ReviewsController` Class (`MovieApi.Controllers.ReviewsController`)

```csharp
[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    // ... (CRUD operations for reviews)

    // Additional operations
    [HttpGet("bymovieid/{movieId}")]
    public IActionResult GetReviewsByMovieId(int movieId) { /* ... */ }

    [HttpGet("byuser"), Authorize]
    public IActionResult GetReviewsByUser() { /* ... */ }
}
```

### App Configuration

#### `MovieApiApp` Class (`MovieApi.Configuration.MovieApiApp`)

```csharp
public class MovieApiApp
{
    // ... (Dependency injection, Swagger, Identity, and middleware configuration)
}
```

## API Requests

### Movies

- **Get All Movies**
  - Endpoint: `GET /api/Movies`

- **Get Movie by ID**
  - Endpoint: `GET /api/Movies/{id}`

- **Update Movie**
  - Endpoint: `PUT /api/Movies/{id}`
  - Authorization: Required

- **Add Movie**
  - Endpoint: `POST /api/Movies`
  - Authorization: Required

- **Delete Movie**
  - Endpoint: `DELETE /api/Movies/{id}`
  - Authorization: Required

### Reviews

- **Get All Reviews**
  - Endpoint: `GET /api/Reviews`

- **Get Review by ID**
  - Endpoint: `GET /api/Reviews/{id}`

- **Update Review**
  - Endpoint: `PUT /api/Reviews/{id}`
  - Authorization: Required

- **Add Review**
  - Endpoint: `POST /api/Reviews`
  - Authorization: Required

- **Delete Review**
  - Endpoint: `DELETE /api/Reviews/{id}`
  - Authorization: Required

- **Get Reviews by Movie ID**
  - Endpoint: `GET /api/Reviews/bymovieid/{movieId}`

- **Get Reviews by User**
  - Endpoint: `GET /api/Reviews/byuser`
  - Authorization: Required

## Analysis and Reflection

### Performance

- **Strengths:**
  - Efficient usage of Entity Framework Core for database interactions.
  - Authentication and authorization mechanisms are built into ASP.NET Core Identity.

- **Considerations:**
  - Implement caching mechanisms for frequently requested data.
  - Optimize database queries for complex operations.

### Scalability

- **Strengths:**
  - ASP.NET Core is designed for scalability.

- **Considerations:**
  - Ensure proper indexing for database tables.
  - Consider implementing a distributed caching strategy for improved performance.

### Security

- **Strengths:**
  - Authentication and authorization using ASP.NET Core Identity.
  - Proper authorization attributes applied to sensitive operations.

- **Considerations:**
  - Regularly update dependencies to address security vulnerabilities.
  - Implement secure coding practices to prevent common web vulnerabilities.

### Maintainability

- **Strengths:**
  - Well-organized code structure and separation of concerns.
  - Utilization of Dependency Injection for better maintainability.

- **Considerations:**
  - Regular code reviews and documentation updates.
  - Maintain up-to-date documentation for API consumers.

---
