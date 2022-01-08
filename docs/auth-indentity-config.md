## enable two factor auth

set **Abp.Identity.TwoFactor.Behaviour** to Forced to use email code confiramtion when login

```json
    "Abp.Identity.TwoFactor.Behaviour": "Forced",

```

## enable reset passsowrd

add settings

```json
  "Abp.Identity.Password.EnableReset": "true",
```
