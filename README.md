# mapsproject3972<br/>
پروژه درس برنامه نویسی پیشرفته ترم 3972<br/>
استاد: دکتر لطفی<br/>
تی ای: محمد بصیری<br/>
اعضای تیم: علی سالمی و محمد پناهی<br/>
<br/>
<br/>
<br/>
<br/>
فایل های سرور<br/>
Users.txt<br/>
مشترک  {“Ali”:{id,pass,name,email,phone,birth,location,gender,code,needtoactive,other}}}<br/>
کاربر  “other”:{“favorite”:{sport:[],team:[]}}<br/>
بازیکن ها  “other”:[team, sport, others] // others مربوط به ورزش مخصوص به خود<br/>
مدیرها  “other”=”sport”<br/>
Teams.txt<br/>
{“team1”: {id,name,sport, score, members:[player1=player1_user],wins, losts, equals, others}, “team2”:…}// others مربوط به ورزش مخصوص به خود<br/>
Competitions.txt<br/>
{id:{sport,teams[ids], date&time,place,title}}<br/>
Leagues.txt<br/>
{league1_id:{sport, teams, rank:{name:score},all_competitions:[], others}, league2:[], …}// others مربوط به ورزش مخصوص به خود<br/>
Othernotifications.txt<br/>
{ user_id :[com_id1, com_id2, …],…}<br/>
فایل های کاربر<br/>
Information.txt<br/>
فقط مخصوص به خود کاربر(برای عدم تکرار بیش از حد درخواست¬ها)<br/>
مشترک  {“persons”:[id,user,pass,name,email,phone,birth,location,gender,code,needtoactive,other]}<br/>
کاربر  “other”:{“favorite”:{sport:[],team:[]}}<br/>
بازیکن ها  “other”:[team, sport, others] // others مربوط به ورزش مخصوص به خود<br/>
مدیرها  “other”=”sport”<br/>
Checked.txt<br/>
{“nots”:[com_id1, com_id2, …]}<br/>
<br/>