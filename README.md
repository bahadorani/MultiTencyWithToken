# MultiTencyWithToken
Multiple databases - complete data isolation

Multi-tenancy means that one instance of the software and all necessary infrastructure serves multiple tenants.
In this project, Users are the common application table but each product has a separate database (in SQL server) and thus complete data isolation and security.
The tenancy is defined in claims of created jwt token. 

Please test it in :
http://localhost:5017/api/values/getusers
or
http://localhost:5017/api/values/getproduct

![image](https://github.com/bahadorani/MultiTencyWithToken/assets/11363979/4275745c-003f-423b-9441-78bae871bed0)


