CR

DEFER INTERP

: GET_LENGTH
   2DUP 2DUP
   0 ROT ROT
   0 DO
      1 + DUP c@ DUP
      125 = IF
         ROT DUP
      0= IF
         DROP DROP LEAVE
      ELSE
         1 - SWAP DROP SWAP
      THEN
         ELSE
            123 = IF
            SWAP 1 + SWAP
         THEN
      THEN
   LOOP
   ROT - SWAP DROP 1 + ;

: COMPARE_ADD
   DROP 2
   s" {+" COMPARE ;

: COMPARE_SUB
   DROP 2
   s" {-" COMPARE ;

: COMPARE_DIV
   DROP 2
   s" {/" COMPARE ;

: COMPARE_MUL
   DROP 2
   s" {*" COMPARE ;

: GET_OPERANDS
   OVER 3 + DUP c@
   123 = IF
      OVER 3 - GET_LENGTH
      SWAP DROP INTERP
      ROT ROT 1 + + DUP c@
      123 = IF
         ROT 3 - GET_LENGTH
         SWAP DROP INTERP
         ROT ROT DROP DROP
      ELSE
         c@ 48 -
      THEN
   ELSE
      DUP c@ 48 -
      SWAP 2 + DUP c@
      123 = IF
         ROT 5 - GET_LENGTH
         SWAP DROP INTERP
         ROT ROT DROP DROP
      ELSE
         c@ 48 -
      THEN
   THEN ;

:noname
   2DUP
   COMPARE_ADD 0= IF GET_OPERANDS + ELSE
   2DUP
   COMPARE_SUB 0= IF GET_OPERANDS - ELSE
   2DUP
   COMPARE_DIV 0= IF GET_OPERANDS / ELSE
   2DUP
   COMPARE_MUL 0= IF GET_OPERANDS * ELSE
   THEN THEN THEN THEN ;
IS INTERP

: PASS
   ." Test passed." ;

: FAIL
   ." Test failed." ;

: CHECK
   = IF PASS ELSE FAIL THEN CR ;

s" {+ 2 3}" INTERP 5 CHECK
s" {- 5 3}" INTERP 2 CHECK
s" {/ 9 3}" INTERP 3 CHECK
s" {* 3 3}" INTERP 9 CHECK

s" {123456}" GET_LENGTH 8 CHECK
s" {1{3{56}8}}" GET_LENGTH 11 CHECK

s" {+ {+ 1 2} 3}" INTERP 6 CHECK
s" {+ 1 {+ 2 3}}" INTERP 6 CHECK
s" {+ {+ 1 1} {+ 1 1}}" INTERP 4 CHECK

s" {* {- 4 1} {+ {/ 3 3} 1}}" INTERP 6 CHECK
