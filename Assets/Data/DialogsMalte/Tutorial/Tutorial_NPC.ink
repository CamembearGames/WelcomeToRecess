EXTERNAL ContinueTutorial()
EXTERNAL CancelTutorial()

-> main

=== main ===

Hallo, bist du der neue Schüler?

 * [Ja (Tutorial starten)]
    Ok, dann lass mich dir alles zeigen
    ~ ContinueTutorial()
    Hier kannst du Tischtennis spielen.
    
    Komm später nochmal vorbei und unterhalte dich mit John.
    ~ ContinueTutorial()
    
    Hier haben wir Janett.
    
    Sie mag Kartenspiele. Wenn du Lust eine Runde hast, sprich einfach mit ihr. 
    ~ ContinueTutorial()
    
    Hier haben wir Janett.
    
    Sie mag Kartenspiele. Wenn du Lust eine Runde hast, sprich einfach mit ihr. 
    ~ ContinueTutorial()
    
    Schlussendlich haben wir hier den Klassenraum. Beim Läuten des Pausengongs solltest du dich auf den Weg dahin machen.

    Selbstverständlich kannst du das auch früher machen. Die Wahl liegt bei dir. 

    ~ CancelTutorial()
    
    
    ->DONE
 * [Nein, ich war letztes Jahr hier]
    Ok, dann sehen wir uns
    ~ CancelTutorial() 
    -> DONE

-> END
