# Cybersecurity Awareness Chatbot - Part 3

## GitHub Actions CI

The build passes with no errors:

![CI Success](ci-success.png)

## Features

### Part 1 Features
- Voice greeting on startup (Kamo.wav)
- ASCII art logo display
- Console-style interaction (converted to GUI)

### Part 2 Features
- GUI with Windows Forms
- WhatsApp style messages with timestamps
- Bot and user avatars (bot.jpeg, user.jpeg)
- Keyword recognition (password, scam, privacy, 2FA)
- Random responses for common questions
- Conversation flow with "another tip"
- Memory to remember user name and interests
- Sentiment detection (worried, curious, frustrated)
- Error handling for invalid inputs

### Part 3 Features (New)
- Task Assistant with MySQL database (Alwaysdata)
  - Add tasks with or without reminders
  - View all tasks in a separate form
  - Complete tasks (mark as done)
  - Delete tasks
- Cybersecurity Quiz Game
  - 11 multiple-choice questions
  - Immediate feedback with explanations
  - Score tracking and leaderboard
  - Average score statistics
- Activity Log
  - Tracks all user actions (tasks, quiz, menu, exit)
  - View recent activity in a separate form
  - Clear logs option
- View Tasks Form (GUI)
  - Complete and delete tasks directly from the form
  - Refresh to see updated task list
- Activity Log Form (GUI)
  - View recent actions with timestamps
  - Clear all logs with confirmation
- Auto-scroll chat (scrolls to bottom automatically)
- Clear Chat button

## Database Setup

The application connects to an **Alwaysdata MySQL database**.

**Connection Details:**
- Host: mysql-prog6221.alwaysdata.net
- Database: prog6221_chatbot_db
- Username: prog6221_app_user
- Port: 3306

**Tables:**
- tasks (id, title, description, reminder_date, status, created_at)
- activity_log (id, action_type, description, timestamp)
- quiz_scores (id, player_name, score, total_questions, date_played)

## How to Run

1. Clone the repository
2. Open KamoChatBotGUI.sln in Visual Studio
3. Restore NuGet packages (MySql.Data version 8.4.0)
4. Press F5 to run

## Commands

| Command | Action |
|---------|--------|
| `help` | Show main menu |
| `add task [title]` | Add a cybersecurity task |
| `add task [title] remind me tomorrow` | Add task with reminder |
| `view tasks` | Show all tasks |
| `complete task [title]` | Mark task as completed |
| `delete task [title]` | Delete a task |
| `play quiz` | Start the cybersecurity quiz |
| `view log` | Show recent activity |
| `scores` | Show leaderboard |
| `task count` | Show task summary |
| `password tips` | Get password safety tips |
| `scam` | Get anti-phishing tips |
| `privacy` | Get privacy protection tips |
| `bye` | Exit the chatbot |

## Buttons

| Button | Action |
|--------|--------|
| View Tasks | Opens task management form |
| Quiz Game | Opens cybersecurity quiz |
| Activity Log | Opens activity log form |
| Clear Chat | Clears all chat messages |
| Send | Sends your message |

## Video Presentation

[YouTube Link]

## Requirements Met

- Minimum 6 commits ✓
- GitHub Actions CI passing ✓
- Release with tag v3.0 ✓
- MySQL database integration (Alwaysdata) ✓
- Task Assistant with reminders ✓
- Quiz Game with 10+ questions ✓
- Activity Log feature ✓
- NLP simulation (keyword detection) ✓
- All Parts 1, 2, and 3 integrated ✓
- WhatsApp style chat with timestamps and avatars ✓

## Author

[Your Name]
Student Number: [Your Student Number]
Module: PROG6221