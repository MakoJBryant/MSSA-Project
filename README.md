# VideoTube
This is a Microsoft Software &amp; Systems Academy project that utilizes the ASP.NET Core 2.2 MVC formate for web apps. The purpose of this project is to get experience with building a website that hosts video media, similar to YouTube.

ASP.NET Core 2.2 MVC web app
 - Published and hosted on Azure.
 - Azure server connection established with use of Azure Key Vault to hide API connection keys.
 - Two Databases used. One for user login/registration and another for everything else the site handles (this is to decrease the amount of sensitive information in transit).
 - Email authentication implemented for user accounts using SendGrid.
 - Used Bootstrap 4 for a majority of the styling and front end development.
 - Created interaction with and connection to Azure Blob Storage (upload, download, create container, and list blobs).
 - Utilized the HTML video tag to make video streaming from Blob storage work on all Internet Browsers.


# Requirements Test Plan

| ID             | Description   | Test Method   | Test Procedure | Current Status  | TimeStamp     |
| -------------  | ------------- | ------------- | -------------  | -------------   | ------------- |
| 1.0 | User Accounts | Demonstration  | Click on register, fill out information, generate authentication token, and store user details in database. Sign in using details provided for account.  | Passing | 29/04/2019 0803 |
| 2.0   | Account Verification  | Demonstration  | After creating an account, go to email provided and click the link sent by the app, which will verify the account and allow the user to login. | Passing | 29/04/2019 0803 |
| 3.0 | Video Storage | Demonstration | Click on the upload button from the home page, fill out video details, select video file to be uploaded and click upload. Video should be saved in blob storage with url to image stored in the database. | Not Passing | 29/04/2019 0803 |
| 4.0 | Video Player | Demonstration | Navigate to specific video url and video should start playing on every major internet browser. | Not Passing | 29/04/2019 0803 |
| 5.0 | Search Bar | Demonstration | Search for a video (will search for title, description, tags, etc.) and appropriate videos will come up. | Not Passing | 29/04/2019 0803 |
| 6.0 | Comments | Demonstration | View comments on a video, post a comment, and edit a comment previously posted. | Not Passing | 29/04/2019 0803 |
| 7.0 | Suggested Videos | Demonstration | Home page will display videos targeted at the user currently logged in. | Not Passing | 29/04/2019 0803 |




