using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RLE
{
    public static class RLECoder
    {
        /// <summary>
        /// Program kodujący i dekodujący string [a-z,A-z] metodą RLE
        /// </summary>
        /// <param name="s">dowolny parametr typu string</param>
        /// <remark>Klasa potrafi kodować i dekodować string metodą RLE oraz sprawdzać czy ciąg składa się tylko z liter</remark>

        public static bool ContainsOnlyLetters(string s)
        {
            bool check = false;
            check = s.All(char.IsLetter);
            return check;
        }

        public static string RLEEncode(string s)
        {
            Debug.Assert(ContainsOnlyLetters(s)); //przed wykonaniem metody odwołaj się do metody sprawdzającej czy ciąg zawiera tylko znaki (prop: IsLetter)
            char[] sArray = s.ToCharArray(); //rozbicie ciągu na jednostki atomowe
            StringBuilder RLEEncoded = new StringBuilder(); //obiekt StringBuilder przechowujący zakodowany ciąg
            int licznik = 0; //licznik wystąpień znaku
            int i = 0; //iterator pętli wewnętrznej
            int j = 0; //iterator pętli zewnętrznej

            //pętla powtarza zliczanie znaków na nowo począwszy od miejsca,
            //w którym ostatnie zliczanie zostało zakończone,
            //do zmiennej j przypisywany jest index, w którym należy zacząć ponowne zliczanie wystąpięń nowego znaku
            for (j = 0; j < sArray.Length; j++)
            {
                licznik = 0; //zerowanie licznika do zliczania wystąpień nowego znaku

                //pętla zlicza liczbę powtarzających się kolejno tych samych znaków,
                //zlicza ich ilość oraz zwiększa j przesuwając index dla pętli zewnętrznej
                for (i = j; i < sArray.Length - 1 && sArray[i] == sArray[i + 1]; i++)
                {
                    licznik++;
                    j = i + 1;
                }


                //blok sprawdza warunki:
                //i!=0  => sprawdza czy drugi znak w tablicy nie jest inny od pierwszego,
                //aby zapobiec porównaniu indexu 0 z nieistniejącym -1
                //
                //sArray.Length > 1 => sprawdza czy ciąg nie jest jednoelementowy,
                //również zapobiega to porównaniu indexu 0 z nieistniejącym -1
                //
                //sArray[i] == sArray[i - 1] => sprawdza ostatni index, którego nie sprawdziła
                //pętla for ze względu na to, że ostatni index 'i' porównywałby nieistniejący index 'i+1' (Length+1)
                //------------(być moze do zoptymalizowania logika pętli wewnętrznej for?)------------- 

                //uwaga: j nie podlega inkrementacji w poniższym warunku, j++ jest wykonane PRZED kolejną iteracją pętli zewnętrznej,
                //czyli j ustawiamy na ostatni SPRAWDZONY element, a przejście do następnego (nowego) wykonuje pętla zewnętrzna
                if (i != 0 && sArray.Length > 1 && sArray[i] == sArray[i - 1])
                {
                    licznik++;
                }

                //Wypisanie do obiektu stringbuilder, warunek wyklucza wpisanie liczby w przypadku wartości 1
                // np. ciąg aab po zakodowaniu jest równy a2b, a nie a2b1
                if (licznik > 1)
                {
                    RLEEncoded.Append(sArray[i] + licznik.ToString());
                }
                else
                {
                    RLEEncoded.Append(sArray[i]);
                }
            }
            return RLEEncoded.ToString();
        }

        public static string RLEDecode(string s)
        {
            char[] sArray = s.ToCharArray(); //konwersja obiektu string do tablicy typu char, rozbicie ciągu na jednostki atomowe
            StringBuilder RLEDecoded = new StringBuilder(); //obiekt StringBuilder przechowujący zakodowany ciąg
            int j = 0; //iterator pętli

            for (j = 0; j < sArray.Length; j++) //pętla iterująca po całej tablicy
            {
                if (j == sArray.Length - 1 && !(char.IsDigit(sArray[j]))) //sprawdź czy jesteś na ostatnim elemencie, 
                                                                          //jeśli nie jest to cyfra to jest to pojedyncze wystąpienie litery
                {
                    RLEDecoded.Append(sArray[j]); //dodaj pojedynczą litere do stringbuildera
                    break; //wyjdź z pętli (koniec stringa)
                }
                if (sArray.Length == 1) //jeśli tablica ma 1 znak, zwróc jeden znak, zakończ pętle
                {
                    RLEDecoded.Append(sArray[j]);
                    break;
                }

                //jeśli dwa znaki obok siebie są różne i drugi z nich nie jest cyfrą to znaczy, że pierwszy wystąpił 1 raz
                else if (sArray[j] != sArray[j + 1] && !(char.IsDigit(sArray[j + 1]))) 
                {
                    RLEDecoded.Append(sArray[j]);
                }

                //jeśli znak jest cyfrą to wstaw znak poprzedzający cyfrę tyle razy ile wynosi cyfra np. b2 => bb
                else if (char.IsDigit(sArray[j + 1]))
                {
                    RLEDecoded.Append(sArray[j], (int)char.GetNumericValue(sArray[j + 1]));
                    j++; //przeskocz za cyfrę, żeby pętla ją ominęła przy następnej iteracji
                }
            }
            return RLEDecoded.ToString();
        }
    }
}
