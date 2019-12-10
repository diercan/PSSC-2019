//
//  main.c
//  Fun
//
//  Created by Robert Pirv on 23/09/2019.
//  Copyright © 2019 Robert Pirv. All rights reserved.
//

#include <stdio.h>
#include<string.h>
void afisare()
{
    printf("suntem in functie\n");
}

int suma(int x, int y)
{
    return x+y;
}


int main() {
    int x=3;
    int y=4;
    afisare();
 //   string z="steaua";
    int s=suma(x,y);
    printf("Suma lui %d + %d=%d\n",x,y,s);
    return 0;
}
