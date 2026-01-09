# Movement API
`"O'zbekiston temir yo'llari" AJ` tizimida harakatni boshqarish tizimidagi xodimlar ishlatayotgan hujjatlarni raqamlashtirish uchun ishlatilgan.

#### Raqamlashtirilgan hujjatlar
1. Du2 jurnali. 
    Stansiya navbatchi(DSP)lari tomonidan stansiyaga kirgan va chiqqandagi ma'lumotlari kiritiladigan jurnal.

2. Du46 jurnali
    Temiryo'l sohasida aniqlangan xatoliklarni kiritadigan SSB jurnali. Stansiya navbatchilari aniqlangan xatoliklarni tegishli xo'jalik dispetcheriga yuboradi. O'z navbatida xo'jalik dispetcheri uni ma'sul ishchilarga yuboradi.

3. Du60/Du61 jurnallari
    Du60 - bu poyezd harakatiga halal beradigan kamchilik kiritiladigan jurnal. Stansiya vs peregonlarda ishchilar tomonidan aniqlangan kamchiliklar kiritiladi. Kamchilik haqidagi ma'lumot va poyezdlar uchun tezlik cheklovlari kiritiladi.

    Du61 blankasi stansiya navbatchisi tomonidan poyezd mashruti bo'yicha du60lar to'plami yig'iladi.

4. Telefonagrammalar
    Peregon(ikki stansiya orasi)larda avtoblokirovka o'chganda stansiya navbatchilari o'zaro aloqa qilish uchun ishlatiladigan tizim.

5. Tekshiruvlar
   Yuqoridagi jurnallar bo'yicha tekshiruvlar tushiradi.


#### Integratsiyalar
1. [Virtual office tizimi](https://cabinet.dasuty.com)
    - Xodimlar ish joylari va ular biriktirilgan rollar haqida ma'lumot olib kelish uchun http methodlardan foydalaniladi. Du46 jurnalidagi o'zgarishlar RabbitMQ broker orqali ma'lumot almashinadi.
2. [Yagona darcha enakl tizimi](https://e-nakl.railway.uz)
    - Enakl tizimining Texkontra moduli orqali Du1 yaratilgandandan so'ng Du2 uchun ma'lumotlar shakllanadi. 2la tizimda ham sostav ustida amallar qilsa bo'ladi va http endpointlar orqali ma'lumotlar bir xilligi ta'minlanadi.# contacts-back
