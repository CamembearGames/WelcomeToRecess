//Jon Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL WateringAcknowledge()
EXTERNAL GoBackToRecess()
EXTERNAL StartPong()

VAR talkAlready = false
VAR JohnFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false

-> main

=== main ===
Bereit für eine Runde? 

Okay, zum Aufwärmen sollten 3 Ballwechsel genügen.

    ~ StartPong()
-> END