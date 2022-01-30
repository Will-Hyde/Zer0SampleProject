## Zer0 Interview Sample Project:

#### Database Design:

Overall, I have implemented Visibility, Status, and Type as their own individual tables so that they can be used as an enumeration, as well as keeping the main Projects table clean and able to be expanded upon. The projects that users belong to are tracked in a "UserProjects" table that uses a composite key of a userId and a projectId (each combination of these two values will be enforced to be unique)
![image](https://user-images.githubusercontent.com/25021249/151682406-bdebbab0-bc79-450f-bd60-aea8d55009a0.png)

The database is hosted in Azure, and normally I would lock it down with firewall restrictions to prevent external access, I have left this open so that others can pull down the project and test it locally.

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
**Useful user info you can use to test with:**
- Will Hyde (UserId: 1, AuthenticationKey: '123456') This one belongs to all projects, except one
- Imaginary Person (UserId: 12, AuthenticationKey: 'null') This one shouldnt belong to any projects
- Vito Corleone (UserId: 9, AuthenticationKey: 'spaghetti') Present in 1 private project
- Emperor Palpatine (UserId: 2, AuthenticationKey: 'iamthesenate') Present in 1 public project

#### Running the Project Locally:
The project can be run using Visual Studio with the .Net Core framework installed (This should come by default upon installation)
Once you open up the project, you can update the NuGet packages to get them installed (Entity framework core, and azure functions SDK)
Other than that it should be relatively straight forward to build and run the app
