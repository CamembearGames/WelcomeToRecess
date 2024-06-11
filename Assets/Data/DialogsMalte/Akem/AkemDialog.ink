//Akem Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL ReAddDialog()

this is a correction

VAR talkAlready = false
VAR AkemFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false

{TimeSlots < 2: 
    {talkAlready:
    (Akem scheint gerade mit etwas anderem beschäftigt zu sein.) 
    ~ ReAddDialog()
        -> END
    
    -else:
        -> Greeting
    }

-else:
    ~ ReAddDialog()
    (Akem ist auf dem Weg zum Klassenraum.)
    -> END
}



== Greeting ==

//~ TimeSlots = TimeSlots + 1
//~ UseTimeSlot(TimeSlots) 
...
...
Hallo. Mein Naeme ist Akem Shariq.
Ich… 
    *Hallo Akem. Ich bin...#Player
        Es freut mich, dich kennenzulernen.
        Was ist dein Hobby?
        -> Hobby
            
	 *Schön, deine Bekanntschaft zu machen#Player
	    …
        Es tut mir leid, ich habe dich nicht verstanden.
        Kannst du… wiederholen?
        -> Bekanntschaft
     * Name wird anders ausgesprochen#Player
        ببخشید 
        Ich lerne noch Deutsch. Ich bin nicht gut. 
        Na-me. Nah-me.
        (Er lächelt verlegen. Das Gespräch scheint hier zu enden.) #Thoughts
        ~ TimeSlots = TimeSlots + 1
        ~ UseTimeSlot(TimeSlots)
        ~ AddEndYearInteraction(0)
        ->END



== Hobby ==
*Erzähle langsam und in simplen Worten
            (Du erzählst sehr langsam und mit einfachem Vokabular von deinen Hobbys)
    	    (Akem hört dir zu und nickt hin und wieder. Hin und wieder kneift er die Augen zusammen und legt seine Stirn in Falten.)#Thoughts
	        (Du bist dir nicht sicher, ob deine Rede ihn interessiert, ob er es versteht oder ob er genervt von deiner Sprechweise ist.)#Thoughts
	        (Am Ende lächelt Akem. Weder du noch er scheint weiteren Gesprächsstoff zu habe.)#Thoughts
	        (Das Gespräch scheint hier zu enden.)#Thoughts
	        ~ TimeSlots = TimeSlots + 1
            ~ UseTimeSlot(TimeSlots)
	        ~ AddEndYearInteraction(0)
	        ->END
*Erzähle in gewohnter Geschwindigkeit und üblichen Wörtern
	        (Du erzählst Akem von deinen Hobbys.)#Thoughts
	        (Du bist dir sicher, dass er nicht mal ein Viertel von deinem Vortrag versteht, aber bei Eigennamen und bestimmten Worten wird er hellhörig und erzählt seine Meinung dazu.)#Thoughts
	        (Von seiner Erzählung verstehst du auch nicht viel, aber bei den Namen von Serien, Büchern oder Spielen kannst du schnell den Faden wieder aufnehmen.)#Thoughts
            (Du lernst, dass er britische Berühmtheiten mag und in seiner Freizeit gerne Dinge für seine Familie macht.) #Thoughts
	        (Aber ab einem gewissen Punkt habt ihr beide keine Konzentration mehr, um das Gespräch aufrecht zu erhalten.)#Thoughts
	        Danke für das Sprechen.  Wir sehen uns. #Player
	        خدا حافظ 
	        ~ AddEndYearInteraction(0)
	        ->END
	        
== Bekanntschaft ==
* Nice to meet you#Player
                (Akem antwortet dir auf Englisch, aber er spricht zu schnell und verwendet Wörter, die du nicht kennst)#Thoughts
                …
                …
                Wir sehen uns. #Player
                خدا حافظ 
                ~ TimeSlots = TimeSlots + 1
                ~ UseTimeSlot(TimeSlots)
                ~ AddEndYearInteraction(0)
                ->END
* Schön dich kennenzulernen.#Player
	            Ah. Ich verstehe. 
	            Schön dich auch kennenzulernen. #Player
            	(Er lächelt verlegen. Das Gespräch scheint hier zu enden.)#Thoughts
                ~ TimeSlots = TimeSlots + 1
                ~ UseTimeSlot(TimeSlots)
            	~ AddEndYearInteraction(0)
	            ->END
	        
	       
