# Paper Prototype - Tiny Arena

## 🎯 Test Doel

Testen of een **willekeurig veranderende arena** genoeg controle biedt aan spelers, ondanks de chaos.

### Key Questions:
- ✅ Voelt het eerlijk aan of te willekeurig?
- ✅ Kunnen spelers strategisch spelen?
- ✅ Is de dynamische omgeving engaging of frustrerend?

---

## 🎮 Design Keuzes

### Doel i.p.v. Vijanden
We gebruiken een **doel** (target) in plaats van vijanden omdat:
- Direct naar vijanden rennen is niet altijd de beste strategie
- **Positionering** wordt belangrijker dan puur combat
- Test variant: Speler kan op afstand schieten (1 tegel van doel)

### Turn-Based i.p.v. Real-Time
Real-time is moeilijk te testen op papier, dus we gebruiken **beurten** om de kern mechanica te evalueren.

---

## 📐 Setup

**Arena:** 11x11 grid

**Materialen:**
- Grid op papier/karton
- 🟢 Groene token = Speler
- 🔴 Rode token = Doel
- 🟠 Oranje markers = Hazards (weggevallen tegels)

---

## 🔄 Beurt Structuur

Elke beurt volgt deze volgorde:

### 1️⃣ Doel Spawnen
- Als er geen doel is → spawn willekeurig
- **Optioneel:** Niet binnen 2 tegels van speler

### 2️⃣ Speler Beweegt
- **Movement:** 2 of 3 stappen (↑↓←→) - *uitest welke beter werkt*
- **Hazard regel:** Als speler op hazard staat → **eerste stap MOET** uit hazard bewegen, anders verlies
- Je mag niet **in** een hazard bewegen

### 3️⃣ Nieuwe Hazards Spawnen
Kies willekeurig 1 van de 3:
- 🟠 **Lijn weg** - Volledige horizontale rij valt weg
- 🟠 **Kolom weg** - Volledige verticale kolom valt weg  
- 🟠 **Random tiles** - 10 willekeurige tegels vallen weg

### 4️⃣ Oude Hazards Terugkomen
- Hazards van **X beurten geleden** komen terug
- *Uitest: 1, 2 of 3 beurten?*

---

## 🎲 Test Variabelen

Tijdens testen kun je aanpassen:

| Variabele | Opties | Doel |
|-----------|--------|------|
| **Movement** | 2 vs 3 stappen | Beste bewegingsvrijheid? |
| **Hazard duur** | 1, 2 of 3 beurten | Goede balans? |
| **Doel afstand** | Adjacent vs 1 tegel range | Combat vs positie focus? |
| **Doel spawn** | Volledig random vs min. 2 tegels weg | Eerlijkheid? |

---

## 📊 Evaluatie Criteria

Na elke test sessie (5-10 beurten):

### Speler Feedback
- **Controle** (1-5): Voelde ik me in control?
- **Eerlijkheid** (1-5): Waren dood-momenten mijn schuld?
- **Engagement** (1-5): Was het spannend?
- **Strategie** (1-5): Kon ik vooruit plannen?

### Observeerder Notities
- Hoe vaak kon speler niet bewegen (trapped)?
- Maakte speler risico's of speelde safe?
- Welke hazard type was meest impactvol?
- Waren er "unfair" momenten?

---

## 📸 Visuele Voorbeelden

> 🟢 = Speler | 🔴 = Doel | 🟠 = Hazard (weggevallen tegel)

### Voorbeeld 1: Willekeurige Tegels Weg
<img width="2514" height="2514" alt="Random tiles hazard" src="https://github.com/user-attachments/assets/ce0a6f40-ed3b-49c0-b927-3cd28d9ea635" />

*10 random tegels zijn weggevallen - speler moet navigeren rond gaten.*

---

### Voorbeeld 2: Lijn Weg
<img width="2795" height="2794" alt="Line hazard" src="https://github.com/user-attachments/assets/39ea53ef-1cd3-4750-ad8f-70db07cba2c6" />

*Horizontale lijn is weg - creëert barrière in arena.*

---

### Voorbeeld 3: Gecombineerde Hazards
![Combined hazards](https://github.com/user-attachments/assets/d9939fb4-ee14-43a8-a2c4-fe99e5be713e)

*Meerdere hazard types actief - maximale chaos en strategic planning vereist.*

