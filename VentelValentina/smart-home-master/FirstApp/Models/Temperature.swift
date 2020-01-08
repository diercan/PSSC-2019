//
//  Temperature.swift
//  FirstApp
//
//  Created by Valentina Vențel on 12/05/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit

class Temperature: NSObject {
   var temperature: Float?
   var data: Date?
    
    
    override init() {}
    
    init(temperature: Float, data: Date) {
        self.temperature = temperature
        self.data = data
    }
    
    override var description: String {
        return "\(data!)"
    }
}
