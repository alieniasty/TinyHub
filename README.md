# TinyHub
Repository for projects and code. Bug report and notification system


TinyHub is a project for school subject named "Basics of Databases". It's written in MVC 6 (.NET CORE 1.0). Frontend is mostly jQuery since
I wasn't familiar with Angular or any Dynamic/Single-page JS framework back then. Now I am :). TinyHub consists of:

- Users authentication system based on Identity Core

![logging](https://cloud.githubusercontent.com/assets/16063923/23951864/d69033d4-098f-11e7-9433-ff04870bae7b.PNG)

- Setting a software project, privately or publicly, in 1 of 4 open-source licenses (MIT, GNU GPL, MPL 2, Apache)

![new project](https://cloud.githubusercontent.com/assets/16063923/23951900/f3f90928-098f-11e7-86d8-4976a73e9528.PNG)

- Uploading files to a project (.rar, .zip, .jpg but also .json, .cs, .java, .class, .py etc.) and downloading them

![project view](https://cloud.githubusercontent.com/assets/16063923/23951937/16f70dd0-0990-11e7-9345-8110600b3ce2.PNG)

- Bug report system, which includes:

  - Description of a bug,
  - It's priority
  - User reporting bug
  - Very sample pdf-generated table with all bugs within single project
  - And of course adding and removing bugs :)
  
  ![bug view](https://cloud.githubusercontent.com/assets/16063923/23951914/04566bd0-0990-11e7-826d-b47d103ec595.PNG)
  
 - Notification System
    
    - Every time a user uploads or removes a file, creates or removes a bug, a notification is born. It appears in the "Feed" menu of
    a single project
    - For each project, notifications are counted so a number of them appears in a little badge in the "Feed" menu option (Facebook style)
    
    ![notifications](https://cloud.githubusercontent.com/assets/16063923/23951972/2d5080e8-0990-11e7-8366-47f00e43eebf.PNG)
    
- Search system

Fell free to pull TinyHub and take a look in code for yourself :)
    
    - Basic search by project's name
    - Advanced project search by all it's fields including the project's start date
  
