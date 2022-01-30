## Zer0 Interview Sample Project:

#### Database Design:

Overall, I have implemented Visibility, Status, and Type as their own individual tables so that they can be used as an enumeration, as well as keeping the main Projects table clean and able to be expanded upon. The projects that users belong to are tracked in a "UserProjects" table that uses a composite key of a userId and a projectId (each combination of these two values will be enforced to be unique)
![image](https://user-images.githubusercontent.com/25021249/151682406-bdebbab0-bc79-450f-bd60-aea8d55009a0.png)

The database is hosted in Azure, and normally I would lock it down with firewall restrictions to prevent external access, I have left this open so that others can pull down the project and test it locally.

#### Project Design:
I decided to implement this API using Azure Functions. Because I really enjoy working with Azure, and I think it satisfies the requirements for the project perfectly.
Within my Azure profile, I have created these resources to host and run the app:
- Zer0 Function App - This is an Azure function app that allows the hosting of the logic behind the API endpoints. Each one is serverless, and able to be scaled independently.
- Zer0 APIM - An API manager that is used to create the public facing API endpoints, and set up any needed rules, authentication, cacheing and load balancing
- Zer0 Database - A SQL database that is used to preserve the project data

Inside the project, I have used Entity Framework to map out the database entities in code. This allows me to query on them from using Lambda functions, treating them like normal C# linq objects. This step wasnt necessary to set up for a snall project like this, but it I think its a good practice to implement for long term projects.

I've kept the authentication portion very simple, mostly to save time. I'm just storing a raw string passphrase in the database and comparing it to what a user supplies in a web request. If this project was for practical business use, I would take the effort to implement a real authentication system (Such as storing hashed/salted passwords in the database, or use JWTs to handle authentication)

Also, I've kept the database connection string in the project to allow people to develop locally. Normally I would take it out and replace it with an Azure Keyvault reference to protect it.

#### Sending Requests to Hosted Azure API:
I have hosted the API on azure using azure API Manager, below are some sample web requests you can use to test it.
###### Sample Postman Requests:
Simply import these into postman as cURL

Get Projects Private
```
curl -X GET \
  https://zer0-apim.azure-api.net/projects/user/1/auth/123456 \
  -H 'cache-control: no-cache' \
  -H 'ocp-apim-subscription-key: a39e9ae11bda4e0dbb1ac7176c83f76a' \
  -H 'postman-token: 0584fb87-d5bf-afc2-e39c-7903504ca9df'
```

Get Projects Public
```
curl -X GET \
  https://zer0-apim.azure-api.net/projects \
  -H 'cache-control: no-cache' \
  -H 'ocp-apim-subscription-key: a39e9ae11bda4e0dbb1ac7176c83f76a' \
  -H 'postman-token: 6eb1f77c-0ab4-e43f-4c0b-88ec29f27c02'
```

Get Private Projects, filtering by User Status and Type
```
curl -X GET \
  'https://zer0-apim.azure-api.net/projects/user/1/auth/123456?Status=3&User=1&Type=3' \
  -H 'cache-control: no-cache' \
  -H 'ocp-apim-subscription-key: a39e9ae11bda4e0dbb1ac7176c83f76a' \
  -H 'postman-token: eaac945a-0f39-f579-0112-d635526ee88c'
```
**Useful user info you can use to test with:**
- Will Hyde (UserId: 1, AuthenticationKey: '123456') This one belongs to all projects, except one
- Imaginary Person (UserId: 12, AuthenticationKey: 'null') This one shouldnt belong to any projects
- Vito Corleone (UserId: 9, AuthenticationKey: 'spaghetti') Present in 1 private project
- Emperor Palpatine (UserId: 2, AuthenticationKey: 'iamthesenate') Present in 1 public project

#### Running the Project Locally:
The project can be run using Visual Studio with the .Net Core framework installed (This should come by default upon installation)
Once you open up the project, you can update the NuGet packages to get them installed (Entity framework core, and azure functions SDK)
Other than that it should be relatively straight forward to build and run the app
