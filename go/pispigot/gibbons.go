package main

import (
	"math"
	"math/big"
)

type LFT struct {
	q int64
	r int64
	s int64
	t int64
}

func decompose(lft LFT) (int64, int64, int64, int64) {
	return lft.q, lft.r, lft.s, lft.t
}

// func add(i1 big.Int, i2 big.Int) big.Int {
// 	var s big.Int
// 	return *s.Add(&i1,&i2)
// }

// func mul(i1 big.Int, i2 big.Int) big.Int {
// 	var s big.Int
// 	return *s.Mul(&i1,&i2)
// }

// func mulint(i1 big.Int, x int) big.Int {
// 	var s big.Int
// 	return *s.Mul(&i1, big.NewInt(int64(x)))
// }

func comp(lft1 LFT, lft2 LFT) LFT {
	q, r, s, t := decompose(lft1)
	u, v, w, x := decompose(lft2)
	return LFT{q*u + r*w, q*v + r*x, s*u + t*w, s*v + t*x}
}

func extr(lft1 LFT, x int64) big.Rat {
	q, r, s, t := decompose(lft1)
	n := q*x + r
	d := s*x + t
	return *big.NewRat(n, d)
}

func lfts() chan LFT {
	c := make(chan LFT)
	go func() {
		for {
			k1 := <-naturals()
			k := int64(k1)
			c <- LFT{k, 4*k + 2, 0, 2*k + 1}
		}
		//close(c)
	}()
	return c
}

func naturals() chan int {
	c := make(chan int)
	go func() {
		for i := int(1); ; i++ {
			c <- i
		}
		//close(c) // unreachable since channel is infinite
	}()
	return c
}

func next(z LFT) int64 {
	e := extr(z, 3)
	f64, _ := e.Float64()
	floor := int64(math.Floor(f64))
	return floor
}

func safe(z LFT, n int64) bool {
	e := extr(z, 4)
	f64, _ := e.Float64()
	floor := int64(math.Floor(f64))
	return n == floor
}

func prod(z LFT, n int64) LFT {
	nlft := LFT{10, -10 * n, 0, 1}
	return comp(nlft, z)
}

func cons(z LFT, zprime LFT) LFT {
	return comp(z, zprime)
}

func pi() chan int64 {
	init := LFT{1, 0, 0, 1}
	return stream(next, safe, prod, cons, init, lfts())
}

func stream(next func(LFT) int64, safe func(LFT, int64) bool, prod func(LFT, int64) LFT, cons func(LFT, LFT) LFT, z LFT, xxs chan LFT) chan int64 {
	c := make(chan int64)
	go func() {
		println("z=", z)
		for {
			y := next(z)
			println("y=", z)
			s := safe(z, y)
			println("s=", s)
			if s {
				c <- y
				z = prod(z, y)
			} else {
				x, _ := <-xxs
				z = cons(z, x)
			}
		}
	}()
	return c
}
