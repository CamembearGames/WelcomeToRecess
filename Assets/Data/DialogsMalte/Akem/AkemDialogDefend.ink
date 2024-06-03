//Akem Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)


VAR talkAlready = false
VAR AkemFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false

{TimeSlots < 2: 
    {talkAlready:
    (Akem scheint gerade mit etwas anderem beschäftigt zu sein.) 
        -> END
    
    -else:
        -> Greeting
    }

-else:
    (Akem ist auf dem Weg zum Klassenraum)
    -> END
}

== Greeting ==

//If the player was nice 
(Akem nickt dir zu, als du dich dem Tisch näherst.)
(Er hält dir ein knuspriges Stück Brot entgegen.)
(Im Gegensatz zu dem, was Emma gesagt hat, kannst du nichts ausergewöhnliches riechen.)
Möchstest du essen? Das ist Naan. 
(Du nimmst die Naan-Hälfte entgegen.)
(Es erinnert dich an einen gebratenen Wrap. Schwarze Samen, die du nicht direkt einordnen kannst, bedecken die Oberfläche.)
(Du beißt hinein.)
(Im Inneren schmeckst du eine Mischung aus Joghurt, Huhn und Gewürzen.)
(Es ist eine echte Geschmacksexplosion in deinem Mund.)
Schön, dass dir es schmeckt!
             ~ AkemFriendship = AkemFriendship + 1          
             ~ UpdateRelashionship("Akem", AkemFriendship)
             
//~ TimeSlots = TimeSlots + 1
//~ UseTimeSlot(TimeSlots) 
...
...
Hallo. Mein Naeme ist Akem Shariq.
Ich… 
    *Hallo Akem. Ich bin...
        Es freut mich, dich kennenzulernen. 
        Was ist dein Hobby?
        -> Hobby
            
	 *Schön, deine Bekanntschaft zu machen
	    …
        Es tut mir leid, ich habe dich nicht verstanden.
        Kannst du… wiederholen?
        -> Bekanntschaft
     * Name wird anders ausgesprochen
        ببخشید 
        Ich lerne noch Deutsch. Ich bin nicht gut. 
        Na-me. Nah-me.
        (Er lächelt verlegen. Das Gespräch scheint hier zu enden.)
        ~ AddEndYearInteraction(0)
        ->END



== Hobby ==
*Erzähle langsam und in simplen Worten
            (Du erzählst sehr langsam und mit einfachem Vokabular von deinen Hobbys)
    	    (Akem hört dir zu und nickt hin und wieder. Hin und wieder kneift er die Augen zusammen und legt seine Stirn in Falten.)
	        (Du bist dir nicht sicher, ob deine Rede ihn interessiert, ob er es versteht oder ob er genervt von deiner Sprechweise ist.)
	        (Am Ende lächelt Akem. Weder du noch er scheint weiteren Gesprächsstoff zu habe.)
	        (Das Gespräch scheint hier zu enden.) 
	        ~ AddEndYearInteraction(0)
	        ->END
*Erzähle in gewohnter Geschwindigkeit und üblichen Wörtern
	        (Du erzählst Akem von deinen Hobbys.)
	        (Du bist dir sicher, dass er nicht mal ein Viertel von deinem Vortrag versteht, aber bei Eigennamen und bestimmten Worten wird er hellhörig und erzählt seine Meinung dazu.)
	        (Von seiner Erzählung verstehst du auch nicht viel, aber bei den Namen von Serien, Büchern oder Spielen kannst du schnell nachvollziehen.)
            (Du lernst, dass er britische Berühmtheiten mag und in seiner Freizeit gerne Dinge für seine Familie macht.) 
	        (Aber ab einem gewissen Punkt habt ihr beide keine Konzentration mehr, um das Gespräch aufrecht zu erhalten.)
	        Danke für das Sprechen.  Wir sehen uns. 
	        خدا حافظ 
	        ~ AddEndYearInteraction(0)
	        ->END
	        
== Bekanntschaft ==
* Nice to meet you
                (Akem antwortet dir auf Englisch, aber er spricht zu schnell und verwendet Wörter, die du nicht kennst)
                …
                …
                Wir sehen uns. 
                خدا حافظ 
                ~ AddEndYearInteraction(0)
                ->END
* Schön dich kennenzulernen.
	            Ah. Ich verstehe. 
	            Schön dich auch kennenzulernen. 
            	(Er lächelt verlegen. Das Gespräch scheint hier zu enden.)
            	~ AddEndYearInteraction(0)
	            ->END
	        
	       
