Potrebne izbojšave:
1) Uporaba zunanjih ključev je pomembna za zagotavljanje celovitosti podatkov. 
V tej implementaciji sem se osredotočil na osnovno funkcionalnost, vendar bi v končni rešitvi dodal ustrezne tuje ključe v "bazo".

2) Trenutna validacija pri ustvarjanju ali posodabljanju naročil temelji predvsem na osnovnih preverjanjih,
kot so dolžina GTIN in prisotnost vrednosti. Implementirati bolj sofisticirane mehanizme validacije, na primer  preverjanje,
ali so produkti v skladu z obstoječimi podatki, ali ali so vsi potrebni podatki za izdelavo naročila (npr. seznam serijskih številk) prisotni.

4) Razširitev testov z enotnimi testi za vsak posamezen servis in logiko ter integracijskimi testi,
ki preverjajo delovanje celotnega sistema. To so testi za preverjanje pravilnosti pošiljanja napak
(kot so BadRequest, NotFound) in delovanje kompleksnih CRUD operacij.

5) Izvajanje operacij nad bazo (npr. SaveChanges) poteka sinhrono, kar lahko povzroči upočasnitev pri večjih količinah podatkov.
Izboljšanje: Prehod na asinhrone operacije z uporabo async/await, zlasti pri izvajanju SaveChangesAsync() v bazi.

Za celotno implementacijo, skupaj s testiranjem napak sem po lastni oceni porabil okoli 9h.


