# 🔐 Identity

> **Кастомная распределённая система идентификации** с безопасным управлением токенами и расширенными механизмами защиты RefreshToken через **асимметричные ключи Ed25519**.

---

## 🧩 Общая информация

Проект реализует:
- 🔑 **Аутентификацию и авторизацию** пользователей;
- 🧱 **Распределённая архитектура**:
  - **Auth** — управление пользователями, токенами и ключами;  
  - **OTP** — генерация и проверка одноразовых кодов;  
  - **Notification** — уведомления пользователей;  
- 🔐 **Распределённая валидация токенов** между сервисами;  
- 🔄 **Ассиметричные ключи шифрования** токенов с их последующей ротацией;
- 🛡️ **Безопасное обновление RefreshToken** с помощью секретного ключа **Ed25519**;  
- 📊 **Ассинхронный и централизованный сбор логов и метрик** через **ElasticSearch** и **Kibana**.

---

## ⚙️ Подготовка к запуску

### Требования
- Установленный [Docker](https://www.docker.com/) и Docker Compose.

> ⚠️ **Важно:**  
> В проекте используются образы **ElasticSearch** и **Kibana**, которые **могут быть недоступны с российских IP-адресов**.  
> Для корректной работы рекомендуется:
> - Включить **VPN**, или  
> - Настроить **проксирование запросов** в системе Docker.

---

### 🚀 Запуск проекта

1. Перейдите в корневую папку проекта.  
2. Выполните команду:

   ```bash
   docker-compose up -d

⚠️ После запуска контейнера сервер будет доступен на http://localhost:7000/swagger

### 🧭 Схема взаимодействия с системой


---

### 1️⃣ Регистрация пользователя

🎯Конечная точка: **/api/identity/sign/up**

Пример:
```
{
  "emailAddress": "test@test.ru",
  "personName": "John",
  "password": "q@kzSq+Kz"
}
```
- emailAddress - почта для регистрации пользователя;
- personName - имя пользователя;
- password - пароль для входа.

Результат: Произойдет создание неактивированного пользователя.

---

### 2️⃣ Активация аккаунта

🎯Конечная точка: **/sign/up/activate/{token}**

- token - токен, который был отправлен на почту для активации акаунта и завершения регистрации, который появится в логах микросервиса Notification.

Результат: После активации аккаунт становится доступен для входа.

---

### 3️⃣ Вход в систему

⚠️ Авторизация выполняется с использованием Ed25519 ключей.

🎯Конечная точка: **/sign/in**

Пример:
```
{
  "emailAddress": "test@test.ru",
  "password": "q@kzSq+Kz",
  "audience": "auth",
  "publicKey": "n_rfYMEjbh0jjRUsJ4bgq0PFceU6dRgiq1Y3OeVkuUw"
}
```
- emailAddress - почта пользователя;
- password - пароль для входа;
- audience - определяет уровень доступа пользователя внутри системы и соответствует аналогии с OAuth2, предоставляя все доступные права в рамках Identity. Устанавливаем ```auth```;
- publicKey - публичный ключ Ed25519 в формате base64url.

Ответ:
```
{
  "content": {
    "id": "492fe391-729c-4371-a61a-3487f535f0dd",
    "type": "email",
    "channel": "te***@test.ru",
    "expiresAt": 1760088736
  },
  "success": true
}
```

Результат: После успешного входа приложение вернет ID для подтверждения входа и информацию о том, куда был отправлен OTP код, который появится в логах микросервиса Notification.

---

### 4️⃣ Подтверждение входа

🎯Конечная точка: **/api/identity/sign/in/confirm**

Пример:
```
{
  "otpId": "492fe391-729c-4371-a61a-3487f535f0dd",
  "otp": "684482"
}
```

- otpId - ID, который был отправлен после запроса на вход;
- otp - код из 6 цифр, который был отправлен по возможному каналу связи.

Ответ:
```
{
  "content": {
    "accessToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6ImE3OWM3MmZjLTM4YjMtNDI1Yi1iZmJhLThiZjc5ZjI4NGUxNCIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI4NGFmNTZlZS1iOTQxLTQwZmUtOGUyZS05M2M2ZDg0YjE4ODIiLCJzaSI6ImJkNzRiNDNmLTU1MDItNDk3My04MjVlLTAyY2IyZTljMTVhOSIsImp0aSI6IjM4ODFkZjQ2LTA1ODMtNDdkMi1hYmI2LTBiY2QwOWU0NjFiZCIsInNjb3BlIjoicmVhZDpwcm9maWxlIGVkaXQ6cHJvZmlsZSIsImV4cCI6MTc1OTc4OTczOSwiaXNzIjoiYXV0aCIsImF1ZCI6ImF1dGgifQ.XqOfezgc0S5-1C4LtlNAYEQadsauUAdyzQzC6UZhsHg33XMKZsdEprFh_cCzMg1kvHhvMkbt4lk7POQ28om_mP2bUv9ZcRuBUpgL28EMucVGDKWIfPKGVMhjM9TdhEL94MUId7EJNLj5Z56gcsQjNepJzowoDpOhghGm-M14n8ofyhkyY1L4ETOL33u_k6lFUEdm6FX2v-RK3KG1mAm3lhiPvoiUUm0fLoAXRDs7c1YaCMAofUiQR3amDRRNTUyyYTiYH5ekYzEreStGvwt9OjVUQr6yJ0RpfY2cVI0IcjMcVASMJ79HcrR9qHbFml9Kp-OcY0Rr1PV6atTNKP9rRg",
    "refreshToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjI2YmQxOWYyLTBjM2ItNDRhMy05NzY0LTI1YTgwODUyNTZhMyIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI4NGFmNTZlZS1iOTQxLTQwZmUtOGUyZS05M2M2ZDg0YjE4ODIiLCJzaSI6ImJkNzRiNDNmLTU1MDItNDk3My04MjVlLTAyY2IyZTljMTVhOSIsImp0aSI6IjM4ODFkZjQ2LTA1ODMtNDdkMi1hYmI2LTBiY2QwOWU0NjFiZCIsImV4cCI6MTc2MjIwNzkzNiwiaXNzIjoiYXV0aCIsImF1ZCI6ImF1dGgifQ.ej42FnHW-TrbDNmo-1D2HkuAZJ2lwb_dhHRVE3x2ptKV4ueSQ2bdyNtRDx82w2psN0_hJiQ7_qD2TS6bCT_URd5vfMEupzDbnX69xGFIfp4_nyE9YcluPnvIodYop-gUt9JcykNqQhHdsWAyTxERffWQorSSx-hfr-OMcHHJKfztUs4HKutOBndag_5QwTHe2DyT5Y6pJJRPJIzDZcGvoq6pzB9wig1hVv7o2u8C5GBtaFbT6qcw1i0AP6XNbf4atQHxvxyD8ZO7EvpD1Ggxnb5X6WqXgzCjc2cfqL0FWGNbn5g5IsTBvBqljHixP96Svpc5qs5Rtycsgz_VRUPgRQ"
  },
  "success": true
}
```

Результат: AccessToken + RefreshToken

---


### 5️⃣ Обновление пары токенов

⚠️ Запрос на обновление подписывается приватным ключом Ed25519.

🎯Конечная точка: **/api/identity/token/refresh**

Пример:
```
{
  "refreshToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjI2YmQxOWYyLTBjM2ItNDRhMy05NzY0LTI1YTgwODUyNTZhMyIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI4NGFmNTZlZS1iOTQxLTQwZmUtOGUyZS05M2M2ZDg0YjE4ODIiLCJzaSI6ImJkNzRiNDNmLTU1MDItNDk3My04MjVlLTAyY2IyZTljMTVhOSIsImp0aSI6IjM4ODFkZjQ2LTA1ODMtNDdkMi1hYmI2LTBiY2QwOWU0NjFiZCIsImV4cCI6MTc2MjIwNzkzNiwiaXNzIjoiYXV0aCIsImF1ZCI6ImF1dGgifQ.ej42FnHW-TrbDNmo-1D2HkuAZJ2lwb_dhHRVE3x2ptKV4ueSQ2bdyNtRDx82w2psN0_hJiQ7_qD2TS6bCT_URd5vfMEupzDbnX69xGFIfp4_nyE9YcluPnvIodYop-gUt9JcykNqQhHdsWAyTxERffWQorSSx-hfr-OMcHHJKfztUs4HKutOBndag_5QwTHe2DyT5Y6pJJRPJIzDZcGvoq6pzB9wig1hVv7o2u8C5GBtaFbT6qcw1i0AP6XNbf4atQHxvxyD8ZO7EvpD1Ggxnb5X6WqXgzCjc2cfqL0FWGNbn5g5IsTBvBqljHixP96Svpc5qs5Rtycsgz_VRUPgRQ",
  "newPublicKey": "hNEoAqtFeHdiLDikeyATYj9RIcyWFRhHiyRRSO0lwtA",
  "timestamp": 1759789522,
  "signature": "Ostpeu6w_U_p48EQh8yGyBngqcZWNiyK99LibC6V_BGpLFgbdKZoxRYJodZBTA_jaQpFrFVRfMEfOZ14KxzOAQ"
}
```

- refreshToken - токен обновления;
- newPublicKey - новый публичный ключ, который будет использоватся для проверки подписи при следующей ротации токенов;
- timestamp - текущее время сервера при отправке запроса (UTC);
- signature - подпись refreshToken+newPublicKey+timestamp секретным ключом, который был создан в паре с публичным ключем для входа или прошлой ротации токенов.

Результат: AccessToken + RefreshToken

---


### 6️⃣ Смена почты

🎯Конечная точка: **/api/identity/change/email/request**

Пример:
```
{
  "newEmailAddress": "test_updated@test.ru"
}
```

- newEmailAddress - новая почта пользователя.

Ответ:
```
{
  "content": {
    "id": "1dd3a0dd-7e80-4c34-aa1b-30caefdeae8c",
    "type": "email",
    "channel": "te***@test.ru",
    "expiresAt": 1760090013
  },
  "success": true
}
```

🎯Конечная точка: **/api/identity/change/email/confirm**

Пример:
```
{
  "otpId": "1dd3a0dd-7e80-4c34-aa1b-30caefdeae8c",
  "otp": "710806"
}
```

- otpId - ID, который был отправлен после запроса на смену почты;
- otp - код из 6 цифр, который был отправлен по возможному каналу связи.

🎯Конечная точка: **/api/identity/change/email/update/{token}**

- token - токен, который был отправлен на новую почту для подтверждения, который появится в логах микросервиса Notification.

Результат: Обновится почта акаунта.

---

### 7️⃣ Смена пароля

🎯Конечная точка: **/api/identity/change/password/request**

Пример:
```
{
  "oldPassword": "q@kzSq+Kz",
  "newPassword": "q@kzSq+Kz!"
}
```

- oldPassword - старый пароль для входа;
- newPassword - новый пароль.

Ответ:
```
{
  "content": {
    "id": "de9aa491-db39-4c23-8a46-b92cf23b0c44",
    "type": "email",
    "channel": "te***@test.ru",
    "expiresAt": 1760091048
  },
  "success": true
}
```

🎯Конечная точка: **/api/identity/change/password/confirm**

Пример:
```
{
  "otpId": "de9aa491-db39-4c23-8a46-b92cf23b0c44",
  "otp": "624140"
}
```

Результат: После успешной операции произойдёт смена пароля пользователя и автоматический выход из всех активных сессий.

---

### 8️⃣ Выход из сессии

⚠️ У данной системы нет моментального отзыва AccessToken. Выходя из сессии - Вы закрываете только возможность получения новых токенов. 
Если необходима такая возможность, то её можно легко реализовать через доменное событие закрытия сессии и храненния ID сессии в черном списке в распределенной базе до истечения срока жизни токена.

🎯Конечная точка: **/api/identity/logout**

Результат: Завершение текущей сессии.

---

### 9️⃣ Выход из всех сессий

🎯Конечная точка: **/api/identity/logout/all**

Результат: Завершение всех активных сессий.
