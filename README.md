# Comments REST API .NET Core 6 & PostgreSQL

If you want to use it, you will need an IDE supported by .Net Core 6 and suitable for your operating system and [PostgreSQL](https://www.postgresql.org/download/). For example, under Windows 10 64-bit, [Visual Studio 2022](https://visualstudio.microsoft.com/) is suitable.

After you have downloaded and installed the appropriate IDE, you need to download the repository itself. The easiest way is to click on the "<>Code" button in the top right corner. And then click on the "Download ZIP" button.

After the file has been downloaded, you must unzip it in a way convenient for you.

After that, go to the folder and run the "Comments.sln" file.

After the file opens in the IDE that you downloaded, you need to run the "Comments" project. For Visual Studio 2022, you need to right-click on the project "Comments" >> "Setup Starter Projects" >> "One Startup Project" >> "Comments".

Then press "F5" on your keyboard.

A new tab will open in the browser.

Wait a couple of minutes, close the tab and run the project again.

Here is the Open API.

Non-authenticating users can only:

1. Register "api/Account/signup"

2. Login "api/Account/signin"

3. View comments "api/Comment"

4. Check if API "api/ping" works

There are ready-made accounts:

1. Administrator : "admin@gmail.com" "admin1"

2. Manager: "manager@gmail.com" "manager1"