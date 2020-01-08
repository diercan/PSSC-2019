//
//  Light.swift
//  FirstApp
//
//  Created by Valentina Vențel on 19/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit

class Light: NSObject {
    var hall: Int?
    var living: Int?
    var kitchen: Int?
    
    override init() {}
    
    init(hall: Int, living: Int, kitchen: Int) {
        self.hall = hall
        self.living = living
        self.kitchen = kitchen
    }
}
