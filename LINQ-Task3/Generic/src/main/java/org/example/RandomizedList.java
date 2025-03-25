package org.example;

import java.util.ArrayList;
import java.util.Random;

public class RandomizedList<T> {
    private final ArrayList<T> items;
    private final Random random;

    public RandomizedList() {
        this.items = new ArrayList<>();
        this.random = new Random();
    }

    public void add(T element) {
        if (random.nextBoolean()) {
            items.add(0, element);
        } else {
            items.add(element);
        }
    }

    public T get(int index) {
        if (items.isEmpty() || index >= items.size()) {
            return null;
        }
        return items.get(random.nextInt(index + 1));
    }

    public boolean isEmpty() {
        return items.isEmpty();
    }
}
