import tweepy
import json

# Twitter API credentials
consumer_key = '************************'
consumer_secret = '*****************************KDXYw1UI'
access_token = '12345689789-************************3b2LPUh0n'
access_token_secret = '**************************************'

auth = tweepy.OAuthHandler(consumer_key, consumer_secret)
auth.set_access_token(access_key, access_secret)
api = tweepy.API(auth, wait_on_rate_limit=True, wait_on_rate_limit_notify=True)
# refer http://docs.tweepy.org/en/v3.2.0/api.html#API
# tells tweepy.API to automatically wait for rate limits to replenish

# Put your search term
searchquery = "election"

users = tweepy.Cursor(api.search, q=searchquery).items()
count = 0
errorCount = 0

file = open('search-election 15 323.json', 'wb')

while True:
    try:
        user = next(users)
        count += 1
        # use count-break during dev to avoid twitter restrictions
        # if (count>10):
        #    break
    except tweepy.TweepError:
        # catches TweepError when rate limiting occurs, sleeps, then restarts.
        # nominally 15 minnutes, make a bit longer to avoid attention.
        print "sleeping...."
        time.sleep(60 * 16)
        user = next(users)
    except StopIteration:
        break
    try:
        print "Writing to JSON tweet number:" + str(count)
        json.dump(user._json, file, sort_keys=True, indent=4)

    except UnicodeEncodeError:
        errorCount += 1
        print "UnicodeEncodeError,errorCount =" + str(errorCount)

print "completed, errorCount =" + str(errorCount) + " total tweets=" + str(count)