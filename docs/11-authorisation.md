# 📘 DOCUMENTATION 3

## `/docs/11-JWT-Authorization.md`

Now once JWT proves identity…

---

## 1️⃣ Role-based authorization

Your token contains:

```json
{
  "role": "Admin"
}
```

So now you can say:

```csharp
[Authorize(Roles = "Admin")]
```

Meaning:

> “Only users with role Admin can access this endpoint”

---

## 2️⃣ How `[Authorize]` actually works

When request hits endpoint:

1. Authentication middleware runs
2. Token validated
3. User.Identity populated
4. Authorization checks:

   * Is user authenticated?
   * Does role match?

If no → 401 / 403

---

## 3️⃣ Current User (VERY important concept)

After JWT validation, you get:

```csharp
User.Identity.Name
User.Claims
```

This allows:

* Secure task ownership
* “Only allow user to see their own tasks”

---

## 4️⃣ Example real-life rule

```csharp
[Authorize]
public IActionResult GetMyTasks()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
```

Now:

* No userId from frontend
* No cheating
* Server decides identity

---
