//Janett Minigame

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL GoBackToRecess()

VAR talkAlready = false
VAR JohnFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 

-> JonMiniGame

== JonMiniGame ==

{ miniGameWin:
Sehr gut, da hast du mich ja richtig abgezogen. Nächste Pause will ich aber eine Revanche, ja?
Dann krieg ich dich richtig dran. Warte es nur ab.
War aber richtig schön mit dir.  
~ GoBackToRecess()
-> END
    -else:
    Yes, Janett bleibt unschlagbar.
    Aber ich hoffe, du hattest trotzdem deinen Spaß. 
    Beinah hättest du mich ja auch gekriegt. Keine Sorge, ich werde niemanden davon erzählen. 
~ GoBackToRecess()
-> END
}
