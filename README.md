# Api Giraffe
### Development still in progress

API Giraffe, an application to test your SOAP and REST API Requests

Based on [SoapUI](https://www.soapui.org/) and [Postman](https://www.getpostman.com/), API Giraffe is an open source API and environment tester.

## Features:
- Send HTTP requests to any URL, saving your request as you make changes to your request.
- Create groups of API calls, allowing you to seperate out calls to your live and test environments.
- Save headers into your API calls, allowing you to authorise with your API in each send.
- (Coming soon) Add custom parameters into your URL's, making it easier to have a {{BaseApiUrl}} or similar.

## How to use:
The application is pretty straight forward to use, however full details can be found below.
#### Creating a request group
To create a request group, simply click on the 'New Group' button at the top of the application. This will open up the new group dialog, which will allow you to enter the name of the group you would like to make. 
Once chosen, simply click the 'Create Group' button inside the dialog. This will automatically create a group and add it to the side bar.
Clicking out of the dialog will cancel the action, allowing you to continue using the application as normal.

#### Adding a new request into a request group
To add a new request start by right-clicking on the request group you would like to add the request to. A menu will be displayed. Clicking on the 'Add Request' button inside the group context menu will show a dialog prompting you to add a name for your request. Clicking on the 'Create Request' button inside the dialog will create the request, add it to the menu under your group and also automatically navigate you to your new request, allowing you to type in your URL and fire off a request when ready.

#### Deleting Dialog Groups
To delete a dialog group, simply right click on the group you would like to delete and click the 'Delete Group' context menu option. This will delete the group and all requests inside.

#### Cancelling Dialogs
Clicking out of the dialog will cancel the action, allowing you to continue using the application as normal.


## Look and Feel
![Screenshot](https://github.com/liamht/Api-Giraffe/raw/master/ScreenShot.JPG "Current UI")
