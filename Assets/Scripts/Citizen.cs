using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen {

    public string name;

    public Need[] needs;

    public Trait trait;

    public bool hasHome;
    
    public int happiness;

    public bool employed;

    public Citizen() {
        GenerateRandomStats();

        CreateNeedArray();

        hasHome = false;
        employed = false;
    }

    void CreateNeedArray() {
        needs = new Need[System.Enum.GetNames(typeof(Needs)).Length - 1];

        for (int i = 0; i < System.Enum.GetNames(typeof(Needs)).Length - 1; i++) {
            needs[i] = new Need((Needs) i);
        }
    }

    public void EnableNeed(Needs need) {
        needs[(int)need].active = true;
    }

    public void CalculateHappiness() {
        int activeNeeds = 1;

        int netHappiness = 100;

        foreach (Need need in needs) {
            if (need.active) {
                activeNeeds++;
                netHappiness += need.amountReceived;
            }
        }

        this.happiness = netHappiness / activeNeeds;
    }

    private void GenerateRandomStats() {
        name = RandomCitizenData.GetRandomName();
        trait = RandomCitizenData.GetRandomTrait();
    }

    static class RandomCitizenData {
        public static string GetRandomName() {
            string result = "";

            string[] firstNames = { "Jami", "Kalle", "Timo", "Saku", "Juhani", "Olavi", "Antero", "Tapani", "Johannes", "Jarno", "Kauko", "Niklas"
                                    , "Mauri", "Kaarlo", "Aukusti", "Jan", "Kai", "Aarne", "Elmeri", "Aki", "Juhana", "Artturi", "Miika", "Riku"
                                    , "Jarno", "Teuvo", "Rauno", "Vilhelm", "Ossi", "Viljo", "Osmo", "Ismo", "Roope", "Atte", "Eelis", "Maria"
                                    , "Helena", "Riitta", "Inkeri", "Ritva", "Sari", "Minna", "Eveliina", "Elisabet", "Laura", "Tarja", "Pirkko"
                                    , "Marika", "Pirkko", "Satu", "Hanna", "Eila", "Kirsi", "Marianne", "Julia", "Amanda", "Arja", "Eila", "Irene"
                                    , "Sisko", "Jaana", "Merja", "Amanda", "Ulla", "Outi", "Milla", "Jonna", "Sini", "Venla", "Vilhelmiina"
                                    , "Margit", "Tiia", "Aada", "Sanni", "Jasmin", "Josefiina", "Taru" };

            int firstName = Random.Range(0, firstNames.Length);

            result += firstNames[firstName];

            string[] lastNames = { "Salonen", "Saarinen", "Sissala", "Pajari", "Korhonen", "Virtanen", "Nieminen", "Makinen", "Makela"
                                    , "Hamalainen", "Laine", "Heikkinen", "Koskinen", "Jarvinen", "Lehtonen", "Lehtinen", "Salminen", "Heinonen"
                                    , "Niemi", "Heikkila", "Kinnunen", "Turunen", "Salo", "Laitinen", "Tuominen", "Rantanen", "Karjalainen"
                                    , "Jokinen", "Mattila", "Savolainen", "Lahtinen", "Ahonen", "Ojala", "Leppanen", "Vaisanen", "Kallio"
                                    , "Hiltunen", "Leinonen", "Miettinen", "Aaltonen", "Pitkanen", "Manninen", "Hakala", "Koivisto", "Anttila"
                                    , "Autio", "Timonen", "Hokkanen", "Kolehmainen", "Lampinen", "Tolonen", "Huhtala", "Nousiainen", "Eskelinen"
                                    , "Rintala", "Kivimaki", "Halme", "Lahteenmaki", "Nylund", "Blomqvist", "Kantola", "Matilainen", "Hytonen"
                                    , "Merilainen", "Auvinen", "Saastamoinen" };
                                    
            int lastName = Random.Range(0, lastNames.Length);

            result += " " + lastNames[lastName];

            return result;
        }

        public static Trait GetRandomTrait() {
            int trait = Random.Range(0, System.Enum.GetNames(typeof(Trait)).Length);

            return (Trait)trait;
        }
    }

    public void PrintCitizenProperties() {
        Debug.Log(name + " is " + trait.ToString());
    }
}