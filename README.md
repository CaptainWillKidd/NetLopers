# NetLopers

Team members:

- Carlos Medina
- Norlando Jr
- Holly Briggs
- Willian Canuto

TRELLO BOARD

https://trello.com/b/gzqE0bLl/netlopers

🧭 Application Route Design — Sunset Scheduler

This document outlines the primary routing structure for the Sunset Scheduler Blazor web application.

These routes define navigation for:

Activity management (CRUD)
Scheduling system
Calendar view
Activity details
📅 Calendar
/calendar

Displays the user's scheduled activities in a calendar format.

Features:

Weekly view of scheduled events
Daily breakdown of activities
Quick navigation to schedule/edit entries
🎯 Activities (CRUD System)
/activities

Displays a list of all available activities.

Features:

Search & filter activities
View activity summaries
Access create/edit/delete actions
/activities/create

Creates a new activity.

Features:

Add activity details (name, category, cost, etc.)
Save to database
/activities/edit/{id}

Edits an existing activity.

Route Parameter:

id → Activity ID

Features:

Update activity information
Modify filters (category, cost, etc.)
/activities/delete/{id}

Deletes an existing activity.

Route Parameter:

id → Activity ID

Features:

Confirmation prompt before deletion
Removes activity from database
/activities/details/{id}

Displays detailed information for a single activity.

Route Parameter:

id → Activity ID

Features:

Full activity breakdown
View scheduling eligibility
Option to schedule activity
📆 Schedule (User Planning System)
/schedule

Displays all scheduled activities for the user.

Features:

List of upcoming events
Filter by date/week
Overview of planned schedule
/schedule/create

Adds an activity to the user’s schedule.

Features:

Select activity
Choose date and time
Save scheduled entry
/schedule/edit/{id}

Edits an existing scheduled activity.

Route Parameter:

id → ScheduledActivity ID

Features:

Modify date/time
Change linked activity
Update notes
/schedule/delete/{id}

Deletes a scheduled activity.

Route Parameter:

id → ScheduledActivity ID

Features:

Confirmation prompt
Removes event from calendar
🧭 Summary Navigation Structure
/calendar
/activities
/activities/create
/activities/edit/{id}
/activities/delete/{id}
/activities/details/{id}

/schedule
/schedule/create
/schedule/edit/{id}
/schedule/delete/{id}

✔ Design Notes
All routes follow REST-style conventions
Activities = reusable data (what exists)
Schedule = user-specific planned instances (what is happening)
Supports full CRUD + calendar integration
Designed for Blazor or MVC routing compatibility
