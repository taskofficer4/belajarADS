# ACTRIS

Urutan yang harus disiapkan saat bikin satu CRUD

1. Create Class DTO - Abstraction/Model/Dto -> class yang akan di consume oleh view
2. Register Auto Mapper - AutoMapperConfig.cs -> pasangkan dengan class hasil generate dari databse

3. Create IRepository - Abstraction/Repositories
4. Create IService - Abstraction/Services

5. Create Query - Infrastructure.EntityFramework/Queries
6. Create Repository - Infrastructure.EntityFramework/Repositories

7. Create Service - Infrastructure/Services

8. Register Repository dan Service pada UnityConfig.cs

9. Create Validator - Infrastructure/Validators

10. Create Controller - Bisa dalam area atau tidak. Sesuai konten
    11.Create View
