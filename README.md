# Forum
Project for ".NET MVC Applications".
Functionalities:
- User:
  - profile management
  - can start topics or reply to existing ones
  - can view other users profiles and send them private messages
    - can manage private topics
  - can report post to moderation
  - can change language 
    - polish, english or german
    - browser language is the default one
  - can change theme
    - 3 themes available
  - can reset password via mail confirmation
  - can use HTML markers in posts (if there are allowed by admin)
- Admin:
  - can manage categories and forums
  - can manage topics and posts
  - can manage users
  - can assign moderators to forums
    - each forum can have many moderators and each moderator can moderate many forums
  - can manage allowed HTML markers
  - can manage unallowed words
  - can add news announcements displayed at the top of main page
- Forum:
  - gathers statistics for each forum
    - views, topics, posts
