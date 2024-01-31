//Jon Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL StartPong()
EXTERNAL GoBackToRecess()


VAR talkAlready = false
VAR JohnFriendship = 5
VAR miniGameWin = true

-> main

=== main ===
    Well done, you should come back later so I can tech you more 
    ~ GoBackToRecess()
-> END