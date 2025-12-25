Slide 1: Title
Title: JWT: JSON Web Tokens
Subtitle: From Concept to Implementation
Visual: Padlock/security icon with modern design

Slide 2: What is JWT?
- JWT = JSON Web Token
- Compact, URL-safe token for authentication & info exchange
- Self-contained, carries claims (user info, roles, permissions)

Slide 3: Why Use JWT?
- Stateless authentication
- Portable across services
- Easy for SPAs, mobile apps, APIs
- Secure (signed, cannot be tampered)

Slide 4: JWT Structure
- Header.Payload.Signature
  - Header: algorithm & type
  - Payload: claims (user info, roles, expiry)
  - Signature: verification with secret/private key
- Diagram: Header + Payload + Signature → JWT

Slide 5: How JWT Works
1. User logs in → server verifies credentials
2. Server generates JWT (signed)
3. Client stores JWT (localStorage / cookie)
4. Client sends JWT in Authorization header
5. Server verifies JWT → grants access
- Diagram: flow from Client ↔ Server with JWT

Slide 6: Security Considerations
- Strong secret keys
- Expiry times
- Use HTTPS
- Optionally refresh tokens

Slide 7: References
- JWT.io: https://jwt.io/introduction/
- RFC 7519: https://www.rfc-editor.org/rfc/rfc7519
- Microsoft Docs: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt

