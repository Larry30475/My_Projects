.data
	menu: .asciiz "Choose an operation:\n1.add\n2.subtract\n3.multiply\n4.divide\n"
	ask: .asciiz "Do you want to continue?"
	arg1: .asciiz "Please, enter first argument:\n"
	arg2: .asciiz "\nPlease, enter second argument:\n"
	zaf: .float 0.0
.text
	lwc1 $f0,zaf
	#start:
	#li $v0,4
	#la $a0,menu
	#syscall
	
	#li $v0,5
	#syscall
	#move $t0,$v0
	
	li $v0,4
	la $a0,arg1
	syscall
	
	li $v0,6
	syscall
	mov.s $f0,$f1
	
	li $v0,4
	la $a0,arg2
	syscall
	
	li $v0,6
	syscall
	mov.s $f0,$f2
	
	#beq $t0,1,addition
	#beq $t0,2,subtract
	#beq $t0,3,multiply
	#beq $t0,4,divide
	
	addition:
	add.s $f12,$f1,$f2
	
	li $v0,2
	syscall