//Jon Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL StartPong()

VAR talkAlready = false
VAR JohnFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 
-> main

=== main ===
Bereit für eine Runde? 

Okay, zum Aufwärmen sollten 5 Ballwechsel genügen.

    ~ StartPong()
-> END