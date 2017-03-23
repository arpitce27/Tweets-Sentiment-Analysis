//insert into donald_tweets (tweet_id, datetime_created, datetime_retweeted, retweeted_text,tweet_text, total_token, positive_words, negetive_words, result)  values (9813,'Tue Nov 15 20:15:19 +0000 2016','Tue Nov 15 19:24:49 +0000 2016','FBI ws investigated by Hillary yrs ago thats dont prompted  corruption election I. ','RT shadylady1031: FBI ws investigated by Hillary yrs ago thats dont prompted  corruption election Ive a.',0,0,0,'');

select * from tbl_tweet where tweet_id = 9813

select count(*) from trump_tweets ;
select count(*) from clinton_tweets;
select * from trump_tweets where tweet_text like'%awesome%'

drop table trump_tweets;
drop table clinton_tweets; 

select * into trump_tweets from tbl_tweets where tweet_text like '%trump%' or tweet_text like '%donald%' or retweeted_text like '%trump%' or retweeted_text like '%donald%' order by tweet_id 
select * into clinton_tweets from tbl_tweets where tweet_text like '%clinton%' or tweet_text like '%hillary%' or retweeted_text like '%clinton%' or retweeted_text like '%hillary%' order by tweet_id 

alter table trump_tweets add total_token Int null, positive_words Int null, negetive_words Int null, result varchar(20) null;

alter table clinton_tweets add total_token Int null, positive_words Int null, negetive_words Int null, result varchar(20) null;


select * from test_table
select * into test_table from trump_tweets where tweet_text like'%awesome%'
update test_table  set retweeted_text ='nothing', datetime_retweeted='nothing'  where tweet_id = 8319

use Twitter_election
select tweet_text,result from trump_tweets where result='positive'
select tweet_text,result from clinton_tweets where result='positive'

use Twitter_election
select * from trump_tweets order by tweet_id;
select * from clinton_tweets order by tweet_id;

select count(*) as Trump_Tweets from trump_tweets ;
select count(*) as Clinton_Tweets from clinton_tweets;

select count(*) as Trump_positive_Tweets, (select count(*) from trump_tweets) as Total_Tweets from trump_tweets where result='positive'
select count(*) as Clinton_positive_Tweets, (select count(*) from clinton_tweets) as Total_Tweets from clinton_tweets where result='positive'
select count(*) as Total_tweets from tbl_tweets
