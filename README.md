# ProductCrudApp - An application which includes crud for both products and categories using Dot Net Core 

# Folder Structure
- Properties
  - launchSettings.json
- Controllers
  - CategoryController.cs
  - ProductController.cs
- Data
  - ApplicationDbContext.cs
- Migrations
  - 20250202153924_initial.cs
  - ApplicationDbContextModelSnapshot.cs
- Model
  - Category.cs
  - Product.cs
- Repository
  - CategoryRepositoryImpl.cs
  - ICategoryRepository.cs
  - IProductRepository.cs
  - ProductRepositoryImpl.cs
- Service
  - CategoryServiceImpl.cs
  - ICategoryService.cs
  - IProductService.cs
  - ProductServiceImpl.cs
- .gitattributes
- .gitignore
- appsettings.json
- Program.cs
- README.md

# Setting Up and Running the Project Locally

# Prerequisites

1) .NET SDK (8) 

2) SQL Server (or any compatible database)

3) Visual Studio or VS Code

4) Postman or any API testing tool

# Steps
1. Clone the Repository
2. Configure Database Connection					:
  a) Open appsettings.json 
  b) Update the ConnectionStrings section with your database details
3. Apply Migrations
4. Run the Project
