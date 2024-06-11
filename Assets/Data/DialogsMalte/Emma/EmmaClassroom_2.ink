//Emma Classroom2

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL WateringAcknowledge()
EXTERNAL GoBackToRecess()

VAR EmmaFriendship = 5
VAR talkAlready = false
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false


{EmmaFriendship < 4:

(Emma scheint nicht glücklich darüber zu sein, dass du dich zu ihr gesetzt hast.)
Eins sei dir gesagt: Wenn du auch nur daran denkst mich während des Unterrichts abzulenken, werde ich *persönlich* dafür sorgen, dass der Elternrat davon erfährt.
(Sie scheint nicht weiter mit dir sprechen zu wollen.)
     -> enddialog 
 
 
    - else:

    Für die nächsten paar Unterrichtseinheiten werden wir wohl in Partnerarbeit lernen müssen.
    Für gewöhnlich übernehme ich einen großen Teil der Arbeit. Ich weiß genau, was die Lehrer sehen wollen und kann die gewünschten Ergebnisse schnell und effektiv hervorbringen.  
    Mit dir habe ich, glaube ich, noch nie im Unterricht kooperiert. Hast du Vorschläge, wie wir unsere kollektive Arbeit organisieren wollen? 
        * Übernehm du die Hauptarbeit, ich unterstütze
            seufz
            Ja, so läuft es immer. So kommen wir zu den besten Ergebnissen.
            Also pass auf: Das ist der Plan.
             ~ EmmaFriendship = EmmaFriendship - 1  
             ~ AddEndYearInteraction(0)
             ~ UpdateRelashionship("Emma", EmmaFriendship)
                -> enddialog
      
        * Lass uns Hälfte-Hälfte machen
            Wie du meinst. Aber wehe ich merke, dass du schlapp machst. Mir ist diese Note sehr wichtig und ich werde mir sie durch nichts nehmen lassen. Verstanden?
             ~ EmmaFriendship = EmmaFriendship + 1  
             ~ AddEndYearInteraction(0)
             ~ UpdateRelashionship("Emma", EmmaFriendship)
              -> enddialog
              
        * Ich übernehme die Hauptarbeit, du unterstützt
            Ich weiß ja nicht. Wenn die Lehrerin das bemerkt, bin ich meine Eins im Sozialverhalten los. 
            Außerdem...
            Ach, weißt du was? Nur durch das Einlassen auf Risiken wird die Wissenschaft nach vorne getrieben. Und wenn ich nochmal am Ende darüber lese, kann ich ja alle Fehler beseitigen.
            Sofern die da sind, natürlich. 
              -> enddialog

}

=== enddialog ===
    ~ GoBackToRecess()
    -> DONE 
