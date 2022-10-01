using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3
{

    class YemekSinifi
    {
        //degişkenler
        public string yemekIcecekAdi; 
        public int adet;
        public double birimFiyat;

        // 3-argument constructor
        public YemekSinifi(string yemekAdi, int adet, double birimFiyat)
        {
            this.yemekIcecekAdi = yemekAdi;
            this.adet = adet;
            this.birimFiyat = birimFiyat;

        }
    } // end class YemekSınıfı
    class Mahalle
    {
        public string mahalleAdi; // mahalle adını temsil eden degisken
        public ArrayList siparislerListesi; //ArrayList
        // 2-argument constructor
        public Mahalle(string mahalleAdi, ArrayList siparislerListesi)
        {
            this.mahalleAdi = mahalleAdi;
            this.siparislerListesi = siparislerListesi;

        }
    } // end class Mahalle

    // TreeNode Sınıfı
    class TreeNode
    {
        public Mahalle mahalle; // Mahalle tipindeki mahalle degiskeni
        public TreeNode leftChild;
        public TreeNode rightChild;

        public void displayNode() // display metodu, Mahalledeki siparişlerin bilgileri yazdırılır
        {
            if (mahalle != null)
            {
                Console.WriteLine("Mahalle adı: " + mahalle.mahalleAdi + " / Teslimat Sayısı: " + mahalle.siparislerListesi.Count);
                Console.WriteLine();
                int siparisSay = 0;
                foreach (List<YemekSinifi> item1 in mahalle.siparislerListesi) //item1 = yemek isimleri
                {
                    Console.WriteLine();
                    Console.WriteLine(++siparisSay + ". Siparişin Bilgileri: ");
                    Console.WriteLine();
                    Console.WriteLine("   Yemek Adı           Adet     Birim Fiyat");
                    Console.WriteLine("---------------      --------   ----------");
                    foreach (YemekSinifi item2 in item1)
                    {
                        Console.Write("{0,15}", item2.yemekIcecekAdi);
                        Console.Write("{0,11}", item2.adet);
                        Console.WriteLine("{0,11}", item2.birimFiyat);
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("******************************************");
        }
    } // end class TreeNsode

    class Tree
    {
        private TreeNode root; // TreeNode tipindeki kök degiskeni

        public int depth = 0; // agacin derinliğine ilk deger olarak 0 atanır

        //constructor
        public Tree() { root = null; }

        public TreeNode getRoot() 
        { return root; } // return kök

        // Agaca bir dügüm eklemeyi saglayan metot
        public void insert(Mahalle newdata)
        {
            TreeNode newNode = new TreeNode(); // newNode nesnesi oluşturulur
            newNode.mahalle = newdata; // newData mahalle tipindedir
            if (root == null) // kök yoksa
                root = newNode; // agacta eleman yoksa ilk eklenen düğüm kök olur
            else // kök varsa
            {
                TreeNode current = root; // currenta kök atanır
                TreeNode parent;
                while (true)
                {
                    parent = current;
                    if (newdata.mahalleAdi.CompareTo(current.mahalle.mahalleAdi) < 0) // yeni eklenecek mahallenin ismi, currentın mahalle isminden alfabetik olarak daha öndeyse
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else  // yeni eklenecek mahallenin ismi, currentın mahalle isminden alfabetik olarak daha sonraysa
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                } // end while
            } // end else
        } // end metot insert()

        // agacin derinliğini hesaplayan metot
        public int maxDepth(TreeNode node)
        {
            if (node == null)

                return -1;
            else
            {
                // her alt ağacın derinliğini hesapla
                int lDepth = maxDepth(node.leftChild);
                int rDepth = maxDepth(node.rightChild);

                // büyük olanı kullan
                if (lDepth > rDepth)
                    return (lDepth + 1);
                else
                    return (rDepth + 1);
            }
        }

        // agaci sırayla dolaşan metot
        public void inOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.displayNode();
                inOrder(localRoot.rightChild);
            }
        }

        // 150 tl üstü siparis alınan siparisleri bulan ve siparis bilgilerin yazdıran metot
        public void YuzElliTlUstuSiparisBilgileriniGetir(string mahalleAdi)
        {
            TreeNode current = root; // currenta kök atanır
            while ((current.mahalle.mahalleAdi).CompareTo(mahalleAdi) != 0) // current.mahalle parametre olarak verilen mahalle degilse istenen mahalleyi bulmak için döngüye girilir
            {
                // parametre olarak verilen mahalle ismine sahip mahalleyi bulmaya yarayan if-else blokları
                if (current.mahalle.mahalleAdi.CompareTo(mahalleAdi) > 0)
                {
                    current = current.leftChild;
                }
                else
                {
                    current = current.rightChild;
                }
            } // end while, parametre olarak verilen mahalle bulunduysa whiledan çıkılmıştır.
            if (current != null)
            {
                int siparisSay = 0;
                foreach (List<YemekSinifi> item1 in current.mahalle.siparislerListesi) //item1 = bir siparişin içerisindeki siparişler generic listi       
                {
                    double siparisTutari = 0;                    

                    foreach (YemekSinifi item2 in item1) // item2 = item1 listesinin içerisindeki sipariş
                    {                        
                        siparisTutari += item2.birimFiyat * item2.adet; // item2deki siparişin tutarı, sipariştutarı degiskenine eklenir
                    }                        
                    if (siparisTutari > 150) // item1deki tüm siparişlerin tutarı hesaplandıktan sonra siparisTutari degiskenindeki deger 150den fazlaysa if bloğuna girilir ve o siparişin bilgileri yazdırılır
                    {                            
                        Console.WriteLine();
                        Console.WriteLine(++siparisSay + ". Siparişin Bilgileri: ");
                        Console.WriteLine();
                        Console.WriteLine("   Yemek Adı           Adet     Birim Fiyat");
                        Console.WriteLine("---------------      --------   ----------");

                        foreach (YemekSinifi siparis in item1)
                        {                               
                                Console.Write("{0,15}", siparis.yemekIcecekAdi);
                                Console.Write("{0,11}", siparis.adet);
                                Console.WriteLine("{0,11}", siparis.birimFiyat);                                
                        }                            
                    }                    
                }
            }
        } // end metot YuzElliTlUstuSiparisBilgileriniGetir

        // d)	Adı verilen yiyeceğin tüm ağaçta kaç adet  sipariş verildiğini hesaplayan ve o yemeğin geçtiği tüm listelerde yemeğin birim fiyatına %10 indirim uygulayan metot 
        public int AgaciDolasYemekSayisiHesaplaİndirimYap(TreeNode localRoot, string yemekAdi)
        {
            double indirimliFiyat = 0;
            foreach (List<YemekSinifi> item1 in localRoot.mahalle.siparislerListesi) //item1 = bir siparişin içerisindeki siparişler generic listi       
            {
                foreach (YemekSinifi item2 in item1) // item2 = item1 listesinin içerisindeki sipariş
                {
                    if (item2.yemekIcecekAdi.CompareTo(yemekAdi) == 0) // item2deki yemek adı parametre olarak verilen yemek adıysa
                    {
                        indirimliFiyat = item2.birimFiyat * 0.9; // o yemeğe yüzde 10 indirim yap                        
                    }                        
                }                
            }
            // yemek sayısını bulmak ve agactaki tüm ismi verilen yemeğe indirim yapmak için tüm agac dolaşılır
            int yemekSayisi = 0;
            if (localRoot != null)  {
                if(localRoot.leftChild != null)
                yemekSayisi += AgaciDolasYemekSayisiHesaplaİndirimYap(localRoot.leftChild, yemekAdi);
                if(localRoot.rightChild != null)
                yemekSayisi += AgaciDolasYemekSayisiHesaplaİndirimYap(localRoot.rightChild, yemekAdi);
                foreach (List<YemekSinifi> item1 in localRoot.mahalle.siparislerListesi)        
                {                    
                    foreach (YemekSinifi item2 in item1)
                    {
                        if (item2.yemekIcecekAdi.CompareTo(yemekAdi) == 0)
                        {
                            yemekSayisi += item2.adet;
                            item2.birimFiyat = indirimliFiyat;
                        }                            
                    }
                }                               
            }
            return yemekSayisi;
        } //end method AgaciDolasYemekSayisiHesapla
    } //end class Tree


    class Program
    {
        static Random random = new Random(); // Random nesnesinin oluşturulması

        static void Main(string[] args)
        {
            string[] mahalleAdi = { "Evka 3", "Özkanlar", "Atatürk", "Erzene", "Kazımdirik"}; //Mahallerin isimlerini tutan dizi
            string[] menuBilgileri = { "Türlü,5", "Pizza,30", "Börek,5","Pilav,15", "Çiğ köfte,11", "Simit,2", "Çorba,10", 
                "Hamburger,35", "Pide,20" ,"Ayran,4", "Kola,5", "Su,2", "Soğuk Çay,4", "Çay,3" }; // yemek bilgilerini tutan dizi

            ArrayList agacListesi = new ArrayList(); // Arraylist oluşturma

            for (int j = 0; j < mahalleAdi.Length; j++) // mahalleadı listesnin uzunluğu kadar döngüye girilir
            {
                ArrayList siparislerListesi = new ArrayList(); // her mahalle için siparislerin tutuldugu bir Arraylist oluşturulur
                 
                int siparisSayisi = random.Next(5, 11); // random nesnesi kullanarak 5 ile 10 aralığında rastgele bir deger siparis sayısı degiskenine atanır.
                YemekSinifi siparis;

                for (int i = 0; i < siparisSayisi; i++)
                {
                    List<YemekSinifi>  siparisBilgileri = new List<YemekSinifi>(); // mahalledeki her siparis birden fazla teslimattan oluşabilir. Her sipariş icin Yemek sınıfı tipinde eleman alan generic list oluşturulur
                    int yemekTuruSay = random.Next(3, 6); // random nesnesi kullanarak 3 ile 5 aralığında rastgele bir deger yemekTuruSay degiskenine atanır.
                    for (int k = 0; k < yemekTuruSay; k++) // yemek türü kadar döngüye girilir
                    {
                        string[] yiyecekIcecekBilgileri = menuBilgileri[random.Next(0, menuBilgileri.Length)].Split(','); // menuBilgileri dizisinden rastgele bir indexindeki degerler split ile ayrılarak bir diziye atanır. Dizideki ilk eleman yemek adı, 2. elemanı fiyat bilgisidir.
                        int yiyecekAdet = random.Next(1, 9); // random nesnesi kullanarak 1 ile 8 aralığında rastgele bir deger yiyecekAdet degiskenine atanır.
                        string yemekAdi = yiyecekIcecekBilgileri[0];
                        double fiyat =  Convert.ToDouble(yiyecekIcecekBilgileri[1]);
                        siparis = new YemekSinifi(yemekAdi, yiyecekAdet, fiyat); // Yemek sınıfı classından nesne oluşturlur
                        siparisBilgileri.Add(siparis); // oluştuulan nesne generic liste eklenir
                    }

                siparislerListesi.Add(siparisBilgileri); // mahalleye ait generic list o mahalleye ait arrayliste eklenir
                }


             agacListesi.Add(new Mahalle(mahalleAdi[j] ,siparislerListesi)); // mahalle nesnesi oluşturulur ve daha sonra agaca eklenecek olan arrayliste eklenir
                
            }

            Tree agac = new Tree(); // agac nesnesi oluşturlur

            foreach (Mahalle item in agacListesi)   // item = agacListesi arraylistindeki mahalleler
            {
                agac.insert(item); // agacListesi arraylistindeki elemanlar agaca eklenir
            }            

            agac.inOrder(agac.getRoot()); // agac inorder dolaşılır ve yazdırılır

            Console.WriteLine("Ağacım derinliği = " + agac.maxDepth(agac.getRoot())); // agacin derinliği yazdırılır
            Console.WriteLine();
            Console.WriteLine();
            foreach (Mahalle item in agacListesi)
            {
                Console.WriteLine();
                Console.WriteLine(item.mahalleAdi + " Mahallesindeki 150 Tlyi Aşan Siparişin Bilgileri:"); // 150 tlyi aşan siparislerin bilgileri yazdırılır
                agac.YuzElliTlUstuSiparisBilgileriniGetir(item.mahalleAdi);
                Console.WriteLine();
                Console.WriteLine("*****************************************");

            }

            agac.AgaciDolasYemekSayisiHesaplaİndirimYap(agac.getRoot(), "Pizza");
            agac.inOrder(agac.getRoot());

            Console.ReadLine();
        }

    }
}
