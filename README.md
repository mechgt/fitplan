# Fit Plan
Fit Plan plugin for SportTracks (Desktop). Schedule and plan workouts to plan your fitness season.

Fit Plan allows you to schedule workouts and it also integrates with the Training Load plugin to help project CTL and TSB values throughout your training plan.  Chart cumulative and actual distance (and time) over the plan, see various summarized statistics (weekly summaries, phase summaries, etc.).

This software was open-sourced after SportTracks desktop was discontinued, and all restrictions have been removed.

Compiled plugin files are available under [Releases](https://github.com/mechgt/fitplan/releases).

![FitPlan](https://mechgt.com/st/images/fp_screenshot.png)

### Overview:
A Fit Plan can be a short term plan for an upcoming 10k or marathon, or a longer term season plan; for example to plan out an entire cycling season.  Fit Plan is designed around phases and workouts.  A Fit Plan contains several phases, and a phase contains several workouts.  Often there may be 4 to 6 phases in a plan (base, build, peak, etc.), however phases can be used to break up portions of your plan in any way you like.

As you design your training plan use the various summary charts and statistics to help you plan, avoid injury, and predict where you'll be as the season progresses and race day approaches.

### Getting Started:
The Fit Plan plugin adds a new view to SportTracks and is accessed through the view menu as shown below:

![FitPlan](https://mechgt.com/st/images/fp_menu.png)

The first time Fit Plan is called up, a default 24 week plan will be created for you.  It's ready to modify to suit your needs.

### How to Setup a plan:
The basic steps are listed below:
1) Decide on the time period for your training plan (when the overall plan starts and ends).  Set the start date.  Set this first because changing start dates later on will cause the entire plan to shift.  Click the calendar icon by the plan name to open the date picker. \
![FitPlan](https://mechgt.com/st/images/fp_dateselect.png)

2) Define phase date ranges starting with the earliest phase and moving forward in time.  Phases are always contiguous, so when one phase ends, the next phase always starts on the following day as shown below. \
![FitPlan](https://mechgt.com/st/images/fp_phases.png) \
Phases can be added or removed as necessary using the task menu or context menus on the calendar.  Select the phase start date on the large calendar, then click Add Phase in either the context menu or under the Fit Plan task items as shown below. \
![FitPlan](https://mechgt.com/st/images/fp_addphase.png)

3) Define workouts.  Like phases, workouts can be added using either the task menu or context menu on the calendar.  Select the workout date on the large calendar, then click Add Workout.

##### Week View tab:
The Week View tab shows you the week at a glance and gives you some summary statistics of the selected week.  The week is chosen by selecting a date on the large planning calendar.  The center date (Wednesday 3/9 below) is the date selected on the calendar, and week view will show 3 days before and after the selected date.  The summary pane contains statistics on the week that's currently in view. \
![FitPlan](https://mechgt.com/st/images/fp_weekview.png)

##### Detail tab:
The detail tab is used to define your Fit Plan, Phases, and Workouts.  Calendar icons will pop up a date picker to select plan/phase/workout start or end dates.

![FitPlan](https://mechgt.com/st/images/fp_detailtab.png)

##### Charts:
**Training Load** - This chart is only available if the Training Load plugin is installed.  Chart data for activities in the past are pulled straight from Training Load, and dates in the future are charted from the plans workout activities.  Enter an estimated Trimp or TSS value for each planned workout to utilized this feature.

![FitPlan](https://mechgt.com/st/images/fp_tlchart.png)

**Distance/Time** - The Distance and Time charts (both daily and cumulative) show actual and planned values over the course of your plan.  For instance, you'll see the planned distance (green line) accumulated over your Fit Plan compared with what you've actually doing (orange line).  This is an indicator of how well you're sticking to your plan.  The Cumulative charts will show an accumulated summary in a line chart format, whereas the other charts will show bars for each planned (green bars) and actual (orange bars) activity.  These same concepts apply equally to the Time and Distance charts.  A vertical blue bar is inserted representing 'today' to give you an idea of where you are in the plan.

### Garmin Fitness Integration:
Fit Plan integrates with the Garmin Fitness plugin to allow you to schedule and manage your detailed Garmin Fitness workouts.  All Garmin Fitness workouts will be automatically available to you in the Workout Library.  Simply drag-n-drop a workout on to the main calendar.

![FitPlan](https://mechgt.com/st/images/fp_workoutlibrary.png)

Garmin Fitness derived workouts can also be scheduled by Fit Plan in the Garmin Fitness plugin.  The advantage of this is that your workouts are scheduled and ready to be transferred to your Garmin device using the Garmin Fitness plugin.  If a workout is linked to a Garmin Fitness template, then it can be scheduled by Fit Plan.  You can see what template a workout is related to in the new Template field shown below.  To be able to schedule, it should have a Garmin Fitness workout icon next to it as shown below.

![FitPlan](https://mechgt.com/st/images/fp_templatelink.png)

If this relationship does not exist for some reason, you can manually create it by dragging your desired Garmin Fitness template from the Library to this field.  This link is automatically established when creating a workout from the Workout Library using a Garmin Fitness workout. 

##### Two ways to schedule:
*Automatic* (recommended): \
Check the box for automatic scheduling, and indicate how many days in advance should be scheduled.  Default is 7 days.  Workouts planned for the next X days will automatically be scheduled in Garmin Fitness for you.

![FitPlan](https://mechgt.com/st/images/fp_autoschedule.png)

*Manual*: \
Right click a Garmin Fitness linked workout on the calendar and select 'Schedule Workout'.

![FitPlan](https://mechgt.com/st/images/fp_manualschedule.png)

NOTE: Only Garmin Fitness derived workouts can be scheduled in Garmin Fitness.

### Fit Plan Files:
Fit Plans can be saved shared with others.  If you've downloaded a Fit Plan file created by another user, you can open it using the Open task item.  Fit Plans are saved in their own file, by default located in the plugin data directory (C:\Documents and Settings\All Users\Application Data\ZoneFiveSoftware\SportTracks\3\Plugins\Data\FitPlan, for example on WinXP)

![FitPlan](https://mechgt.com/st/images/fp_opensave.png)

Each Fit Plan is saved in it's own file.  For instance, you can have a 10k Fit Plan, a Marathon Fit Plan, etc.  Click New Fit Plan in order to start a new file, or Open to open a downloaded file.  When opening Fit Plan files created by another user, you may need to adjust the dates.  Simply change the Fit Plan start date and the entire plan will slide to your new start date.  This makes plans portable and re-usable from race to race.  For instance, say you created a plan for a race in March, and you'd like to re-use the same plan for a race in July.  Simply change the Fit Plan start date and all phases and workouts will shift to the new summer dates.  This same concept would apply to Fit Plans shared by others.

![FitPlan](https://mechgt.com/st/images/fp_dateselect.png)
