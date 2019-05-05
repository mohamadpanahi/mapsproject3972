# mapsproject3972
پروژه درس برنامه نویسی پیشرفته ترم 3972
استاد: دکتر لطفی
تی ای: محمد بصیری
اعضای تیم: علی سالمی و محمد پناهی




فایل های سرور
Users.txt
مشترک  {“Ali”:{id,pass,name,email,phone,birth,location,gender,code,needtoactive,other}}}<br/>
کاربر  “other”:{“favorite”:{sport:[],team:[]}}
بازیکن ها  “other”:[team, sport, others] // others مربوط به ورزش مخصوص به خود
مدیرها  “other”=”sport”
Teams.txt
{“team1”: {id,name,sport, score, members:[player1=player1_user],wins, losts, equals, others}, “team2”:…}// others مربوط به ورزش مخصوص به خود
Competitions.txt
{id:{sport,teams[ids], date&time,place,title}}
Leagues.txt
{league1_id:{sport, teams, rank:{name:score},all_competitions:[], others}, league2:[], …}// others مربوط به ورزش مخصوص به خود
Othernotifications.txt
{ user_id :[com_id1, com_id2, …],…}
فایل های کاربر
Information.txt
فقط مخصوص به خود کاربر(برای عدم تکرار بیش از حد درخواست¬ها)
مشترک  {“persons”:[id,user,pass,name,email,phone,birth,location,gender,code,needtoactive,other]}
کاربر  “other”:{“favorite”:{sport:[],team:[]}}
بازیکن ها  “other”:[team, sport, others] // others مربوط به ورزش مخصوص به خود
مدیرها  “other”=”sport”
Checked.txt
{“nots”:[com_id1, com_id2, …]}
