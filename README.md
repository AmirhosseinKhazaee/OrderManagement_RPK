

# سیستم مدیریت سفارشات (Order Management API)

یک Web API با معماری Clean Architecture برای مدیریت مشتریان، سفارشات و آیتم‌های سفارش.

---

# توضیح پروژه

این پروژه یک سیستم مدیریت سفارشات است که امکان:
- مدیریت مشتریان
- مدیریت سفارشات
- مدیریت آیتم‌های سفارش
- گزارش‌گیری از مشتریان برتر
را فراهم می‌کند.

---

# معماری پروژه

پروژه بر اساس Clean Architecture طراحی شده است:



src/  
├── API → لایه ارائه (Controllers)  
├── Application → DTOs / Services / Interfaces / Validation  
├── Domain → Entities (Customer, Order, OrderItem)  
├── Infrastructure → EF Core / DbContext / Repositories



---

# تکنولوژی‌ها

- ASP.NET Core Web API
- Entity Framework Core
- SQLite Database
- FluentValidation
- Clean Architecture
- Repository Pattern
- Dependency Injection

---

# ساخت دیتابیس (Migration)

برای ساخت و بروزرسانی دیتابیس، دستور زیر را اجرا کنید:

```bash
dotnet ef database update --project .\src\Infrastructure
````

---

# اجرای پروژه

برای اجرای API:

```bash
dotnet run --project src/API
```

سپس Swagger:

```
http://localhost:xxxx/swagger
```

---

# Data Seeding

در این پروژه یک Data Seeder پیاده‌سازی شده است که هنگام اجرای برنامه:

- اگر دیتابیس خالی باشد
    
- داده‌های تستی (Customer, Order, OrderItem) ایجاد می‌کند
    

این کار برای تست راحت API و گزارش‌ها انجام شده است.

---

# قابلیت‌های سیستم

## مشتریان (Customer)

- ایجاد مشتری
    
- ویرایش مشتری
    
- حذف مشتری (در صورت نداشتن سفارش)
    
- دریافت لیست مشتریان (با Pagination)
    

---

## سفارشات (Order)

- ایجاد سفارش به همراه آیتم‌ها
    
- ویرایش سفارش
    
- حذف سفارش
    
- دریافت لیست سفارش‌ها (با Pagination)
    
- دریافت جزئیات سفارش
    

---

## آیتم‌های سفارش (OrderItem)

- ثبت به همراه سفارش
    
- شامل محصول، تعداد و قیمت واحد
    

---

# گزارش‌ها

##  مشتریان برتر

نمایش مشتریانی که بیشترین مبلغ خرید را داشته‌اند:

```
GET /api/reports/customers-top
```

خروجی شامل:

- نام مشتری
    
- ایمیل
    
- مجموع خرید
    

---

# Validation ها

در پروژه از FluentValidation استفاده شده است:

- ایمیل معتبر باشد
    
- Quantity باید بزرگتر از 0 باشد
    
- UnitPrice باید بزرگتر از 0 باشد
    
- فیلدهای ضروری نباید خالی باشند
    

---

# Pagination

برای جلوگیری از بار زیاد روی دیتابیس:

## Customers

```
GET /api/customers?page=1&pageSize=10
```

## Orders

```
GET /api/orders?page=1&pageSize=10
```

---

# تست API

برای تست از Swagger استفاده کنید:

```
http://localhost:xxxx/swagger
```

---

# نمونه اجرای کامل پروژه

1. ساخت دیتابیس:
    

```bash
dotnet ef database update --project .\src\Infrastructure
```

2. اجرای API:
    

```bash
dotnet run --project src/API
```

3. تست در Swagger
    

---

# نکات طراحی

- استفاده از Clean Architecture برای جداسازی لایه‌ها
    
- استفاده از Repository Pattern برای دسترسی به داده
    
- استفاده از DTO برای جلوگیری از exposure مستقیم Entity
    
- مدیریت Validation در لایه Application
    
- استفاده از Data Seeding برای داده تستی
    

---

# هدف پروژه

این پروژه برای تمرین و نمایش مهارت‌های زیر طراحی شده است:

- طراحی معماری تمیز
    
- کار با Entity Framework Core
    
- طراحی RESTful API
    
- مدیریت داده و روابط پیچیده
    
- پیاده‌سازی validation و pagination
    
- طراحی گزارش‌های ساده
    

---
