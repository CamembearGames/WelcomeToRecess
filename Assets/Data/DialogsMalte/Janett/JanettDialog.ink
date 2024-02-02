//Janett Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
VAR talkAlready = false
VAR JanettFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 

{TimeSlots < 2: 
    {talkAlready:
        War schön mit dir zu reden. Wollen wir das nächste Pause wiederholen?
        -> END
    
    -else:
        -> Greeting
    }

-else:
    We should go to class, recess is over
    -> END
}


== Greeting ==
 
{ JanettFriendship: 
- 1: Passt auf, Leute. Loseralarm. 
- 2: Hey Mädels, Idiot auf zwölf Uhr.
- 3: Kannst du das glauben? Hm, ist was? Ach, nein, ist nicht wichtig. 
- 4: Tja, wen haben wir denn da?
- 5: Hallöchen, siehst du schick aus.
- 6: Dein Referat gestern war richtig toll.
- 7: Na du, willst du dich nicht zu uns setzen?
- 8: Psst, willst du wissen, wer heimlich auf Tim steht?
- 9: Mädels, macht mal Platz für ihn/sie- 
- 10: Hey, hast du Lust, nach der Schule mit mir ins Kino zu gehen?
}

* [Sprechen] Worum geht es gerade?
    ~ talkAlready = true
    ~ UpdateTalkAlready("Janett", talkAlready)
    ~ TimeSlots = TimeSlots + 1
    ~ UseTimeSlot(TimeSlots)
    -> JanettTalking
* [Stapelstecker spielen] 
    {TimeSlots == 0: 
        Wollen wir gemeinsam Karten spielen?
        -> JanettMiniGame
    -else: 
        We don't have time to play cards.
        -> Greeting
    }

* [Verlassen]
    -> END

== JanettTalking ==

{ JanettFriendship < 4:
Sorry, ich und meine Mädels sind gerade beschäftigt. Das könnte noch eine Weile dauern, also lass uns erstmal in Ruhe, ja?
    -> END
- else:
Komm, gesell dich zu uns. Wir haben einiges zu bereden. 
Wir gehen gerade all die Sachen durch, die wir über die Ferien so erfahren haben. 
Kannst du glauben, dass Emma die ganze Zeit nur zu Hause gelieben ist? Ich hab von keiner einzigen Person gehört, die sie irgendwo gesehen hat. Kino, Konzerte, nichts!
Aber ich will ja auch nicht lästern. Jeder wie er will, oder?
Wie steht es mit dir? Hast du dir schon einen Eindruck von unserer Klasse gemacht?
Was hälst du zum Beispiel von Jon? Dort drüber an der Tischtennisplatte?
    * [Positive Anmerkung] Er macht eigentlich einen ganz netten Eindruck 
    Er ist großartig, oder? So nett, so hilfsbereit, so... menschlich? Macht das Sinn?
    Außerdem so gutaussehend. Aber das ist ja offenkundlich.
    Ich bin echt froh, mit ihm auszugehen. 
    Aber wie ich sehe, hast du ein gutes Auge für Menschen. Wir sollten öferts solche Gespräche führen. Mir jukt es unter den Fingern, deine Meinung zu unseren anderen Mitschülern zu hören. 
        ~ JanettFriendship = JanettFriendship + 1
        ~ UpdateRelashionship("Janett", JanettFriendship)
    -> END
    
    
    * [Negative Bemerkung] Schlau ist er ja nicht gerade.
    
    Sag mal, geht's noch? Wie kannst du nur so etwas gemeinen sagen?
    Jon ist wirklich nett und gibt sich echt Mühe, bei allem was er macht.
    Ich hätte nicht von dir erwartet, dass du so fies bist. Aber scheinbar ist nicht alles Gold was glänzt. 
        ~ ChangeRelashionship("John", -1) 
        ~ UpdateRelashionship("Janett", JanettFriendship) 
        
    -> END
    
    * [Skandalöses Gerücht] Ich habe gehört, dass er nur so viel Sport macht, um sich danach stundenlang im Spiegel anzusehen. 
    Nein, nein, nein, wo hast du das denn gehört? Das stimmt ja überhaupt nicht.
    Ich bin mit ihm jetzt schon zwei Wochen zusammen. Ich weiß *alles* über ihn, und das ist einfach nur falsch.
    Du solltest echt aufpassen, dass du keine falschen Behauptungen in Unlauf bringst.
    Zum Glück hast du es mir erzählt. So können wir das noch richtig stellen, bevor dir das noch irgendwer glaubt.
        ~ JanettFriendship = JanettFriendship - 1
        ~ UpdateRelashionship("Janett", JanettFriendship)
    -> END
}


== JanettMiniGame ==

{ JanettFriendship < 4:
Hmm... Weißt du, mir ist gerade nicht so danach.  
    -> END
- else:
    ~ JanettFriendship = JanettFriendship + 1
    ~ UpdateRelashionship("John", JanettFriendship)
    ~ talkAlready = true
    ~ UpdateTalkAlready("John", talkAlready)
    ~ TimeSlots = TimeSlots + 2
    ~ UseTimeSlot(TimeSlots)
    Supi, dann lass uns mal anfangen. 
    ~ StartMiniGame(0)
// Minigame
-> END
}
